using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Api.Dto;

public class AuthenticateDto
{
    [Required]
    public string EMail { get; set; }
    [Required]
    public string Password { get; set; }
}
