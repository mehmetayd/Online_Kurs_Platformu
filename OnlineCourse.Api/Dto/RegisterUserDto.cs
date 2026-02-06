using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Api.Dto;

public class RegisterUserDto
{
    [Required]
    public string EMailAddress { get; set; }
    [Required]
    public string Password { get; set; }
}