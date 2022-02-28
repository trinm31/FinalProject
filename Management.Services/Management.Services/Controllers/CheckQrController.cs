using AutoMapper;
using Management.Services.Dtos;
using Management.Services.Services.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class CheckQrController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CheckQrController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet("[action]/{studentId}")]
    public async Task<IActionResult> GetQrCode(string studentId)
    {
        var getStudentQr = await _unitOfWork.Student.GetFirstOrDefaultAsync(e => e.StudentId == studentId);
        
        if (getStudentQr == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<CheckQrDto>(getStudentQr));
    }
}