using AutoMapper;
using Management.Services.DbContext;
using Management.Services.Dtos;
using Management.Services.Models;
using Management.Services.Services.Repository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class RoomsController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger _logger;
    private readonly RoomRepository _roomRepository;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public RoomsController(
        IWebHostEnvironment webHostEnvironment,
        ILogger<RoomsController> logger,
        RoomRepository roomRepository,
        ApplicationDbContext db,
        IMapper mapper
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _roomRepository = roomRepository;
        _db = db;
        _mapper = mapper;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> RoomsPagination([FromQuery] PaginationDto paginationDto)
    {
        var values = _db.Rooms
            .OrderBy(x=>x.CourseId)
            .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
            .Take(paginationDto.PageSize)
            .ToList();

        return Ok(values);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var results = await _roomRepository.GetAll();
        return Ok(results);
    }
    
    // [HttpGet("[action]")]
    // [AllowAnonymous]
    // public async Task<IActionResult> Test()
    // {
    //     var results = _db.Schedules.ToList();
    //     List<Room> rooms = new List<Room>();
    //     foreach (var result in results)
    //     {
    //         rooms.Add(new Room()
    //         {
    //             CourseId = result.CourseId,
    //             Name = result.CourseId
    //         });
    //     }
    //     await _roomRepository.CreateRange(rooms);
    //     return Ok(results);
    // }
    
    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var results = await _roomRepository.Read(id);
        if (results == null)
        {
            return NotFound();
        }
        return Ok(results);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);
        if (ModelState.IsValid)
        {
            await _roomRepository.Create(room);
        }
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Edit([FromBody] RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);
        if (ModelState.IsValid)
        {
            await _roomRepository.Update(room);
        }
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roomRepository.Delete(id);
        return Ok();
    }
}