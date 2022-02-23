using Management.Services.Services.Repository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class StaffAssignController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger _logger;
    private readonly StaffAssignRepository _staffAssignRepository;
    
    public StaffAssignController(
        IWebHostEnvironment webHostEnvironment,
        ILogger<StaffAssignController> logger,
        StaffAssignRepository staffAssignRepository
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _staffAssignRepository = staffAssignRepository;
    }
    
    [HttpGet("[action]/{roomId:int}/{staffId}")]
    public async Task<IActionResult> Assign(string staffId, int roomId)
    {
        await _staffAssignRepository.Assign(staffId, roomId);
        return Ok();
    }
    
    [HttpGet("[action]/{roomId:int}/{staffId}")]
    public async Task<IActionResult> UnAssign(string staffId, int roomId)
    {
        await _staffAssignRepository.UnAssign(staffId, roomId);
        return Ok();
    }
}