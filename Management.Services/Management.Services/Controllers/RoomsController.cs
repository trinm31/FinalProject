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
    
    public RoomsController(
        IWebHostEnvironment webHostEnvironment,
        ILogger<RoomsController> logger,
        RoomRepository roomRepository
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _roomRepository = roomRepository;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var results = await _roomRepository.GetAll();
        return Ok(results);
    }
    
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
    public async Task<IActionResult> Create([FromBody] Room room)
    {
        if (ModelState.IsValid)
        {
            await _roomRepository.Create(room);
        }
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Edit([FromBody] Room room)
    {
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