using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Data;
using UserManagement.Services.Models;
using IdentityModel;
using UserManagement.Services.Dtos;

namespace UserManagement.Services.Controllers;

[Route("api/[controller]")]
[ApiController] 
[Authorize(Roles = "Admin")] 
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(IMapper mapper, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _db = db;
        _userManager = userManager;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> UsersPagination([FromQuery] PaginationDto paginationDto)
    {
        var values = _db.Users
            .OrderBy(x=>x.FirstName)
            .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
            .Take(paginationDto.PageSize)
            .ToList();
        foreach (var user in values)
        {
            var userTemp = await _userManager.FindByIdAsync(user.Id);
            var roleTemp = await _userManager.GetRolesAsync(userTemp);
            user.Role = roleTemp.FirstOrDefault();
        }
        return Ok(_mapper.Map<List<UserDto>>(values));
    }

    [HttpGet("[action]/{studentId}")]
    public IActionResult GetUserById(string studentId)
    {
        if (studentId == null)
        {
            return BadRequest();
        }

        var result = _db.Users.Find(studentId);
        return Ok(_mapper.Map<UserDto>(result));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateUser([FromBody] CreateApplicationUserDto createApplicationUserDto)
    {
        var usernameCheck = _db.Users.Where(u => u.UserName == createApplicationUserDto.Username);
        if (!usernameCheck.Any())
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = createApplicationUserDto.Username,
                    Email = createApplicationUserDto.Username,
                    EmailConfirmed = true,
                    FirstName = createApplicationUserDto.FirstName,
                    LastName = createApplicationUserDto.LastName,
                    PhoneNumber = createApplicationUserDto.PhoneNumber,
                    Position = createApplicationUserDto.Position,
                    PersionalId = createApplicationUserDto.PersionalId
                };

                var result = await _userManager.CreateAsync(user, createApplicationUserDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, createApplicationUserDto.RoleName);

                    await _userManager.AddClaimsAsync(user, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, createApplicationUserDto.Username),
                        new Claim(JwtClaimTypes.Email, createApplicationUserDto.Username),
                        new Claim(JwtClaimTypes.FamilyName, createApplicationUserDto.FirstName),
                        new Claim(JwtClaimTypes.GivenName, createApplicationUserDto.LastName),
                        new Claim(JwtClaimTypes.Role, createApplicationUserDto.RoleName)
                    });

                    return Ok(new {success = true, message = "Create User Successfully"});
                }

                return Ok(new {success = false, message = "Create User Fail"});
            }
        }
        return Ok(new {success = false, message = "Username Already Existed"});
    }

    [HttpPost("[action]")]
    public IActionResult UpdateUser([FromBody] UpdateApplicationUserDto updateApplicationUserDto)
    {
        var userDb = _db.Users.FirstOrDefault(u => u.Id == updateApplicationUserDto.Id);
        if (ModelState.IsValid)
        {
            userDb.FirstName = updateApplicationUserDto.FirstName;
            userDb.LastName = updateApplicationUserDto.LastName;
            userDb.Position = updateApplicationUserDto.Position;
            userDb.PhoneNumber = updateApplicationUserDto.PhoneNumber;
            userDb.PersionalId = updateApplicationUserDto.PersionalId;
            _db.Users.Update(userDb);
            _db.SaveChanges();
            return Ok();
        }

        return NotFound();
    }

    [HttpGet("[action]/{studentId}")]
    public async Task<IActionResult> LockUnlock(string studentId)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var claimUser = _db.Users.FirstOrDefault(u => u.Id == claims.Value);

        var applicationUser = _db.Users.FirstOrDefault(u => u.Id == studentId);
        if (applicationUser == null)
        {
            return Ok(new { success = false, message = "Error while Locking/Unlocking" });
        }

        if (claimUser.Id == applicationUser.Id)
        {
            return Ok(new { success = false, message = "Error You are currently lock your account" });
        }

        if (applicationUser.LockoutEnd != null && applicationUser.LockoutEnd > DateTime.Now)
        {
            //user is currently locked, we will unlock them
            applicationUser.LockoutEnd = DateTime.Now;
        }
        else
        {
            applicationUser.LockoutEnd = DateTime.Now.AddYears(1000);
        }

        _db.SaveChanges();
        return Ok(new { success = true, message = "Operation Successful." });
    }

    [HttpDelete("{studentId}")]
    public async Task<IActionResult> Delete(string studentId)
    {
        var applicationUser = _db.Users.Find(studentId);
        if (applicationUser == null)
        {
            return Ok(new { success = false, message = "Error while Deleting" });
        }

        _db.Users.Remove(applicationUser);
        _db.SaveChanges();
        return Ok(new { success = true, message = "Delete successful" });
    }
}