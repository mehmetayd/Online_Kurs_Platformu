using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Kurs_Platformu.Data;
using OnlineCourse.Api.Authenticator;
using OnlineCourse.Api.Dto;
using OnlineCourse.Api.Extensions;

namespace OnlineCourse.Api.Controllers;

public class AuthenticateController: OnlineCourseControllerBase
{
    private readonly OnlineCourseDbContext _dbContext;
    private readonly IJwtTokenService _tokenService;

    public AuthenticateController(OnlineCourseDbContext dbContext, IJwtTokenService tokenService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Authenticate(AuthenticateDto authenticateDto)
    {
        var user = _dbContext
            .Users
            .SingleOrDefault
            (
                x => x.EMailAddress == authenticateDto.EMail &&
                    x.PasswordHash == authenticateDto.Password.ToHash()
            );

        if(user == null)
        {
            return NotFound();
        }

        var token = _tokenService.CreateToken(user.Id.ToString(), user.EMailAddress);

        return Ok
        (
            new JwtTokenDto()
            {
                AccessToken = token,
                TokenType = "Bearer"
            }
        );
    }
}
