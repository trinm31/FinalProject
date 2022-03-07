using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Data;

namespace UserManagement.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin)]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;
    
    public UsersController(IMapper mapper, ApplicationDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }
    
    
}