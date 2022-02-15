using Management.Services.DbContext;
using Management.Services.Messages;
using Management.Services.Models;
using Management.Services.RabbitMQSender;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class SettingController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IRabbitMQManagementMessageSender _rabbitMqManagementMessageSender;

    public SettingController(ApplicationDbContext db, IRabbitMQManagementMessageSender rabbitMqManagementMessageSender)
    {
        _db = db;
        _rabbitMqManagementMessageSender = rabbitMqManagementMessageSender;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetSetting()
    {
        var setting = _db.Settings.First();
        return Ok(setting);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(Setting setting)
    {
        if (ModelState.IsValid)
        {
            _db.Settings.Update(setting);
            _db.SaveChanges();

            SchedulingSettingMessage schedulingSettingMessage = new SchedulingSettingMessage()
            {
                StartDate = setting.StartDate,
                EndDate = setting.EndDate,
                ExternalDistance = setting.ExternalDistance,
                InternalDistance = setting.InternalDistance,
                NoOfTimeSlot = setting.NoOfTimeSlot,
                ConcurrencyLevelDefault = setting.ConcurrencyLevelDefault
            };
            
            try
            {
                _rabbitMqManagementMessageSender.SendMessage(schedulingSettingMessage, "schedulingupdatesettingmessagequeue");
            }
            catch (Exception e)
            {
                throw;
            }
            
            return Ok();
        }

        return BadRequest();
    }
}