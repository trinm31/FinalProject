using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Management.Services.Dtos;

public class StudentCreateDto
{
    public string StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
}