using System.Linq.Dynamic.Core;
using Management.Services.DbContext;
using Management.Services.Dtos;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public ScheduleRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateRange(List<Schedule> schedules)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        _db.AddRange(schedules);
        _db.SaveChanges();
    }

    public async Task DeleteAll()
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var schedules = _db.Schedules.ToList();
        _db.RemoveRange(schedules);
        _db.SaveChanges();
    }

    public async Task<ScheduleDto> GetAll()
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var schedules = _db.Schedules.OrderBy(x=> x.Date).ToList();
        var setting = _db.Settings.First();
        var scheduleDto = new ScheduleDto();
        scheduleDto.FirstDay = new DateTime(schedules.First().Date.Year, schedules.First().Date.Month,
            schedules.First().Date.Day);
        var events = new List<Events>();

        var listDate = schedules.Select(d => d.Date).Distinct().ToList();
        int id = 1;
        for (int i = 0; i < listDate.Count; i++)
        {
            for (int j = 0; j < setting.NoOfTimeSlot; j++)
            {
                if (j == 0)
                {
                    var noOfExamInOneDay = schedules.Where(s => s.Date == listDate[i] && s.Slot == j).ToList();
                    if (noOfExamInOneDay.Count == 0)
                    {
                        continue;
                    }
                    var date = new DateTime(listDate[i].Date.Year, listDate[i].Date.Month,
                        listDate[i].Date.Day);
                    var @event = new Events();
                    @event.Id = id;
                    foreach (var exam  in noOfExamInOneDay)
                    {
                        @event.Title += " " + exam.CourseId;
                    }
                    @event.Start = date.AddHours(7);
                    @event.End = date.AddHours(9);
                    events.Add(@event);
                    id++;
                }else if (j == 1)
                {
                    var noOfExamInOneDay = schedules.Where(s => s.Date == listDate[i] && s.Slot == j).ToList();
                    if (noOfExamInOneDay.Count == 0)
                    {
                        continue;
                    }
                    var date = new DateTime(listDate[i].Date.Year, listDate[i].Date.Month,
                        listDate[i].Date.Day);
                    var @event = new Events();
                    @event.Id = id;
                    foreach (var exam  in noOfExamInOneDay)
                    {
                        @event.Title += " " + exam.CourseId;
                    }
                    @event.Start = date.AddHours(9).AddMinutes(30);
                    @event.End = date.AddHours(11).AddMinutes(30);
                    events.Add(@event);
                    id++;
                }
                else if (j == 2)
                {
                    var noOfExamInOneDay = schedules.Where(s => s.Date == listDate[i] && s.Slot == j).ToList();
                    if (noOfExamInOneDay.Count == 0)
                    {
                        continue;
                    }
                    var date = new DateTime(listDate[i].Date.Year, listDate[i].Date.Month,
                        listDate[i].Date.Day);
                    var @event = new Events();
                    @event.Id = id;
                    foreach (var exam  in noOfExamInOneDay)
                    {
                        @event.Title += " " + exam.CourseId;
                    }
                    @event.Start = date.AddHours(13);
                    @event.End = date.AddHours(15);
                    events.Add(@event);
                    id++;
                }
                else
                {
                    var noOfExamInOneDay = schedules.Where(s =>s.Date == listDate[i] && s.Slot == j).ToList();
                    if (noOfExamInOneDay.Count == 0)
                    {
                        continue;
                    }
                    var date = new DateTime(listDate[i].Date.Year, listDate[i].Date.Month,
                        listDate[i].Date.Day);
                    var @event = new Events();
                    @event.Id = id;
                    foreach (var exam  in noOfExamInOneDay)
                    {
                        @event.Title += " " + exam.CourseId;
                    }
                    @event.Start = date.AddHours(15).AddMinutes(30);
                    @event.End = date.AddHours(17).AddMinutes(30);
                    events.Add(@event);
                    id++;
                
                }
               
            }
            
        }
        scheduleDto.Events=events;

        return scheduleDto;
    }
}