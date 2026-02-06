using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.UI.Dto;

public class AuthenticateUserDto
{
    [Required]
    public string EMail { get; set; }
    [Required]
    public string Password { get; set; }
}
