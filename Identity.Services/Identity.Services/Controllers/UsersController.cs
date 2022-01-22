// using System.Security.Claims;
// using Identity.Services.Data;
// using Identity.Services.Models;
// using IdentityServerHost.Quickstart.UI;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Identity.Services.Controllers;
//
// [SecurityHeaders]
// [Authorize(Roles = SD.Admin)]
// public class UsersController : Controller
// {
//     private readonly ApplicationDbContext _db;
//     private readonly UserManager<IdentityUser> _userManager;
//     private readonly RoleManager<IdentityRole> _roleManager;
//
//     public UsersController(ApplicationDbContext db, UserManager<IdentityUser> userManager,
//         RoleManager<IdentityRole> roleManager)
//     {
//         _db = db;
//         _userManager = userManager;
//         _roleManager = roleManager;
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> Index()
//     {
//         var claimsIdentity = (ClaimsIdentity) User.Identity;
//         var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
//         var userList = _db.Users.Where(u => u.Id != claims.Value);
//         foreach (var user in userList)
//         {
//             var userTemp = await _userManager.FindByIdAsync(user.Id);
//             var roleTemp = await _userManager.GetRolesAsync(userTemp);
//             user.Role = roleTemp.FirstOrDefault();
//         }
//         ViewData["Message"] = TempData["Message"];
//         return View(userList);
//     }
//
//     public async Task<IActionResult> Delete(string id)
//     {
//         var applicationUser = _db.Users.Find(id);
//         if (applicationUser != null)
//         {
//             await _userManager.DeleteAsync(applicationUser);
//             TempData["Message"] = "Success: Delete successfully";
//         }
//
//         return RedirectToAction(nameof(Index));
//     }
//
//     // [HttpGet]
//     // public async Task<IActionResult> Edit(string id)
//     // {
//     //     var user = _db.Users.Find(id);
//     //     var usertemp = await _userManager.FindByIdAsync(user.Id);
//     //     var roleTemp = await _userManager.GetRolesAsync(usertemp);
//     //     user.Role = roleTemp.FirstOrDefault();
//     //     if (user.Role == SD.Student)
//     //     {
//     //         return RedirectToAction(nameof(EditStudent), new {id = id});
//     //     }
//     //     else
//     //     {
//     //         return RedirectToAction(nameof(EditLecturer), new {id = id});
//     //     }
//     //
//     //     return NoContent();
//     // }
//     //
//     // [HttpGet]
//     // public async Task<IActionResult> EditStaff(string id)
//     // {
//     //     var user = _db.Users.Find(id);
//     //     if (user == null)
//     //     {
//     //         ViewData["Message"] = "Error: User not found";
//     //         return NotFound();
//     //     }
//     //
//     //     return View(user);
//     // }
//     //
//     // [HttpPost]
//     // [ValidateAntiForgeryToken]
//     // public async Task<IActionResult> EditStaff(ApplicationUser user)
//     // {
//     //     if (user == null)
//     //     {
//     //         ViewData["Message"] = "Error: Data null";
//     //         return RedirectToAction(nameof(Index), "Users");
//     //     }
//     //
//     //     if (!ModelState.IsValid)
//     //     {
//     //         return RedirectToAction(nameof(Index));
//     //     }
//     //
//     //     _db.Users.Update(user);
//     //     _db.SaveChanges();
//     //     return RedirectToAction(nameof(Index), "Users");
//     // }
//     //
//     // [HttpGet]
//     // public async Task<IActionResult> EditStudent(string id)
//     // {
//     //     var user = _db.Users.Find(id);
//     //     if (user == null)
//     //     {
//     //         ViewData["Message"] = "Error: User not found";
//     //         return NotFound();
//     //     }
//     //
//     //     return View(user);
//     // }
//     //
//     // [HttpPost]
//     // [ValidateAntiForgeryToken]
//     // public async Task<IActionResult> EditStudent(StudentProfile user)
//     // {
//     //     if (user == null)
//     //     {
//     //         ViewData["Message"] = "Error: Data null";
//     //         return RedirectToAction(nameof(Index), "Users");
//     //     }
//     //
//     //     if (!ModelState.IsValid)
//     //     {
//     //         return RedirectToAction(nameof(Index));
//     //     }
//     //
//     //     _db.Users.Update(user);
//     //     _db.SaveChanges();
//     //     return RedirectToAction(nameof(Index), "Users");
//     // }
//     
//     // public async Task<IActionResult> ForgotPassword(string id)
//     // {
//     //     var user = _db.Users.Find(id);
//     //     if (user == null)
//     //     {
//     //         return View();
//     //     }
//     //
//     //     ForgotPasswordViewModel UserEmail = new ForgotPasswordViewModel()
//     //     {
//     //         Email = user.Email
//     //     };
//     //     return View(UserEmail);
//     // }
//     //
//     // [HttpPost]
//     // [ValidateAntiForgeryToken]
//     // public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
//     // {
//     //     if (ModelState.IsValid)
//     //     {
//     //         var user = await _userManager.FindByEmailAsync(model.Email);
//     //         if (user != null)
//     //         {
//     //             var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//     //             var result = await _userManager.ResetPasswordAsync(user, token, "Password123@");
//     //             if (result.Succeeded)
//     //             {
//     //                 return View("ResetPasswordConfirmation");
//     //             }
//     //         }
//     //     }
//     //     return View("ResetPasswordFail");
//     // }
// }
