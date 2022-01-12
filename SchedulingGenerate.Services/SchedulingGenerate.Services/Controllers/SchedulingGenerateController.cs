using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchedulingGenerate.Services.BackgroundService;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.MatrixHelper;
using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Controllers
{
    [ApiController]
    [Route("api/SchedulingGenerate")]
    public class SchedulingGenerateController: ControllerBase
    {
        public static DateTime StartDate = DateTime.Today;
        public static DateTime EndDate = DateTime.Today.AddDays(31);
        public static int MaxScheduleDays = (int)(EndDate - StartDate).TotalDays;
        public static int NoOfTimeSlots = 4;
        public static int[,] concurrencyLevel = new int[MaxScheduleDays,NoOfTimeSlots];
        public static int ConcurrencyLevelDefault = 10;
        public static int D2 = 1; // external distance 10 - 4 
        public static int D1 = 1;
        public static HashSet<Student> Students = new HashSet<Student>();
        public static HashSet<StudentCourse> StudentCourses = new HashSet<StudentCourse>();
        public static HashSet<Course> CoursesDb = new HashSet<Course>();
        public static Graph graph;
        public static HashSet<Node> AllNodesHashSet;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SchedulingGenerateController( BackgroundWorkerQueue backgroundWorkerQueue, IServiceScopeFactory serviceScopeFactory)
        {
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await Schedule();
            });
           
            return Ok();
        }

        private async Task Schedule()
        {
            var begin = DateTime.Now;
            
            GetDataFromDb();

            var adj = BuildMatrix();
            
            PrintMatrix(ref adj);
            
            Console.WriteLine("Matrix build success " + DateTime.Now);
            
            var c = graph.SortAdjMatrix();
            
            Schedule(c);

            PrintSchedule();
            
            StoreScheduleResult();
            
            var end = DateTime.Now;
            
            Console.WriteLine($"Finish!!!! Take: {end-begin}");
        }
        
        private void GetDataFromDb()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                
                Students = db.Students.AsNoTracking().ToHashSet();
                CoursesDb = db.Courses.AsNoTracking().ToHashSet();
                StudentCourses = db.StudentCourses.AsNoTracking().ToHashSet();
                
                var result = db.Results.ToList();
                db.RemoveRange(result);
                db.SaveChanges();
            }

        }

        private void StoreScheduleResult()
        {
            List<Result> results = new List<Result>();
            int[] indexArray = new int[MaxScheduleDays * NoOfTimeSlots];

            int init = 0;
            for (int i = 0; i < MaxScheduleDays * NoOfTimeSlots; i++)
            {
                indexArray[i] = init;
                init++;
            }

            // calculated date and slot
            for (int i = 0; i < AllNodesHashSet.Count; i++)
            {
                int indexOfColor = Array.IndexOf(indexArray, AllNodesHashSet.ElementAt(i).Color);
                int numberOfDate = indexOfColor / NoOfTimeSlots;
                int numberOfSlot = indexOfColor % NoOfTimeSlots;
                Result result = new Result()
                {
                    CourseId = AllNodesHashSet.ElementAt(i).Id,
                    Color = AllNodesHashSet.ElementAt(i).Color,
                    Date = StartDate.AddDays(numberOfDate),
                    Slot = numberOfSlot
                };
                results.Add(result);
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                db.Results.AddRange(results);
                db.SaveChanges();
            }
        }

        private int?[,] BuildMatrix()
        {
            graph = new Graph();
            
            foreach (var course in CoursesDb)
            {
                graph.CreateNode(course.Id);
            }

            AllNodesHashSet = graph.AllNodes.ToHashSet();
            var count = 0;
            
            foreach (var course in graph.AllNodes)
            {
                var set1 = StudentCourses.Where(c => c.CourseId == course.Id).ToList();
                
                foreach (var courseToCompare in graph.AllNodes)
                {
                    if (course.Id != courseToCompare.Id)
                    {
                        var weight = (from sc in set1
                            join sctc in StudentCourses.Where(c => c.CourseId == courseToCompare.Id) 
                                on sc.StudentId equals sctc.StudentId
                            select sc).Count();
                        
                        if (weight > 0)
                        {
                            course.AddArc(courseToCompare, weight);
                            Console.WriteLine($"Number of Arc: {count++}");
                        }
                    }
                }   
            }
            
            return graph.CreateAdjMatrix();
        }
        
        private void Schedule(HashSet<Node> c)
        {
            SetDefaultValueCl();
            
            int[,] colors = new int[MaxScheduleDays,NoOfTimeSlots];
            SetDefaultValueColor(colors);
            
            var noOfColoredCourses = 0;
            
            for (int i = 0; i < c.Count; i++)
            {
                if (noOfColoredCourses == c.Count) 
                {
                    break;
                }

                
                if (c.ElementAt(i).Color == -1)
                {
                    int? rab;
                    if (i == 0)
                    {
                        rab = GetFirstNodeColor(c.ElementAt(i), colors);
                        if (rab == null)
                        {
                            break;
                        }
                    }
                    else
                    {
                        rab = GetSmallestAvailableColor(c.ElementAt(i), colors);
                    }
                        
                    if (rab != null)
                    {
                        c.ElementAt(i).Color = (int)rab;
                        noOfColoredCourses++;
                        Console.WriteLine($"Process loading:{(double)noOfColoredCourses/c.Count}");
                        int a = -1;
                        int b = -1;
                        for (int e = 0; e < MaxScheduleDays; e++)
                        {
                            for (int f = 0; f < NoOfTimeSlots; f++)
                            {
                                if (colors[e,f] == rab)
                                {
                                    a = e;
                                    b = f;
                                    break;
                                }
                            }
                        }
                        
                        concurrencyLevel[a, b] -= c.ElementAt(i).CL;
                    }
                    
                }

                var m = GetOrderedAdjacencyCourseOf(c.ElementAt(i));
                for (int j = 0; j < m.Count; j++)
                {
                    int? rcd;
                    if (m.ElementAt(j).Color == -1 )
                    {
                        rcd = GetSmallestAvailableColor(m.ElementAt(j), colors);
                        if (rcd != null)
                        {
                            m.ElementAt(j).Color = (int)rcd;
                            noOfColoredCourses++;
                            Console.WriteLine($"Process loading:{(double)noOfColoredCourses/c.Count}");
                            int a = -1;
                            int b = -1;
                            for (int e = 0; e < MaxScheduleDays; e++)
                            {
                                for (int f = 0; f < NoOfTimeSlots; f++)
                                {
                                    if (colors[e, f] == rcd)
                                    {
                                        a = e;
                                        b = f;
                                        break;
                                    }
                                }
                            }

                            concurrencyLevel[a, b] -= m.ElementAt(j).CL;
                        }
                    }
                }

            }
        }

        private HashSet<Node> GetOrderedAdjacencyCourseOf(Node c)
        {
            List<Node> listNodeNotSorted = new List<Node>();
            List<Node> listNodeSorted = new List<Node>();

            // copy list node
            for (int i = 0; i < c.Arcs.Count; i++)
            {
                listNodeNotSorted.Add(c.Arcs.ElementAt(i).Child);
            }
           
            listNodeSorted.AddRange(listNodeNotSorted.OrderByDescending(x => x.Arcs.Count)
                .ThenByDescending(x => x.MaxWeight())
                .ThenBy(x => x.Id));
          
            return listNodeSorted.ToHashSet();
        }
        
        private void SetDefaultValueCl()
        {
            for (int i = 0; i < MaxScheduleDays; i++)
            {
                for (int j = 0; j < NoOfTimeSlots; j++)
                {
                    concurrencyLevel[i, j] = ConcurrencyLevelDefault;
                }
            }
        }

        private void SetDefaultValueColor(int[,] colors)
        {
            int init = 0;
            for (int i = 0; i < MaxScheduleDays; i++)
            {
                for (int j = 0; j < NoOfTimeSlots; j++)
                {
                    colors[i, j] = init;
                    init++;
                }
            }
        }
        
        private int? GetFirstNodeColor(Node c,int[,] colors)
        {
            for (int j = 0; j < MaxScheduleDays; j++)
            {
                for (int k = 0; k < NoOfTimeSlots; k++)
                {
                    if (concurrencyLevel[j,k] > c.CL)
                    {
                        return colors[j,k];
                    }
                }
            }
            return null;
        }
        
        private int? GetSmallestAvailableColor(Node c,int[,] colors)
        {
            var alArc = c.Arcs;
            for (int j = 0; j < MaxScheduleDays; j++)
            {
                for (int k = 0; k < NoOfTimeSlots; k++)
                {
                    var valid = true;
                    for (int r = 0; r < alArc.Count; r++)
                    {
                        var @ref = alArc.ElementAt(r).Child.Color;
                        if (@ref != -1)
                        {
                            int a = -1;
                            int b = -1;
                            for (int e = 0; e < MaxScheduleDays; e++)
                            {
                                for (int f = 0; f < NoOfTimeSlots; f++)
                                {
                                    if (colors[e, f] == @ref)
                                    {
                                        a = e;
                                        b = f;
                                        break;
                                    }
                                }
                            }

                            if (a != j || b != k)
                            {
                                // external distance between 2 color
                                if (Math.Abs(j-a) < D2)
                                {
                                    // Internal distance between 2 color
                                    if (Math.Abs(k-b) <= D1)
                                    {
                                        valid = false;
                                        break;
                                    }
                                }

                                if (concurrencyLevel[j,k] <= c.CL)
                                {
                                    valid = false;
                                    break;
                                }

                                if (CheckExamsConstraint(c,j,colors) == false)
                                {
                                    valid = false;
                                    break;
                                }
                            }
                            else
                            {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (valid)
                    {
                        return colors[j,k];
                    }
                }
            }
            return null;
        }

        private bool CheckExamsConstraint(Node c, int j, int[,] colors)
        {
            var studentRegisted = StudentCourses.Where(sc => sc.CourseId == c.Id);
            foreach (var studentRegis in studentRegisted)
            {
                var counter = 0;
                for (int q = 0; q < NoOfTimeSlots ; q++)
                {
                    var crs = AllNodesHashSet.Where(n => n.Color == colors[j,q]);
                    foreach (var node in crs)
                    {
                        var scTmp = new StudentCourse()
                        {
                            CourseId = node.Id,
                            StudentId = studentRegis.StudentId
                        };
                        //var exist = StudentCourses.OrderBy(c=>c.Id).Any(s => s.CourseId == node.Id && s.StudentId == studentRegis.StudentId);
                        var exist = StudentCourses.Contains(scTmp);
                        if (exist)
                        {
                            counter++;
                            // number of slot allow in 1 day
                            if (counter == 2)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        
        private void PrintMatrix(ref int?[,] matrix)
        {
            Console.Write("       ");
            for (int i = 0; i < graph.AllNodes.Count(); i++)
            {
                Console.Write("{0}  ", graph.AllNodes.ElementAt(i).Id);
            }

            Console.WriteLine();

            for (int i = 0; i <  graph.AllNodes.Count(); i++)
            {
                Console.Write("{0} | [ ", graph.AllNodes.ElementAt(i).Id);

                for (int j = 0; j <  graph.AllNodes.Count(); j++)
                {
                    if (i == j)
                    {
                        Console.Write(" &,");
                    }
                    else if (matrix[i, j] == null)
                    {
                        Console.Write(" .,");
                    }
                    else
                    {
                        Console.Write(" {0},", matrix[i, j]);
                    }

                }
                Console.Write(" ]\r\n");
            }
            Console.Write("\r\n");
        }

        private void PrintSchedule()
        {
            for (int i = 0; i < AllNodesHashSet.Count; i++)
            {
                Console.WriteLine($"Course: {AllNodesHashSet.ElementAt(i).Id} - And Color: {AllNodesHashSet.ElementAt(i).Color}");
            }
        }
    }
}