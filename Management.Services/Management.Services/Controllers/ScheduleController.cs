using AutoMapper;
using Management.Services.DbContext;
using Management.Services.RabbitMQSender;
using Management.Services.Services.IRepository;
using Management.Services.Services.Repository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class ScheduleController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger _logger;
    private readonly ScheduleRepository _scheduleRepository;
    
    public ScheduleController(
        IWebHostEnvironment webHostEnvironment,
        ILogger<ScheduleController> logger,
        ScheduleRepository scheduleRepository
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _scheduleRepository = scheduleRepository;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var values = await _scheduleRepository.GetAll();
        return Ok(values);
    }
    
}