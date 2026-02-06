using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Kurs_Platformu.Data;
using OnlineCourse.Api.Authenticator;
using OnlineCourse.Api.Dto;
using OnlineCourse.Api.Extensions;

namespace OnlineCourse.Api.Controllers;

public class UserController : OnlineCourseControllerBase
{
    private readonly OnlineCourseDbContext _dbContext;

    public UserController(OnlineCourseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterUserDto registerUserDto)
    {
        var user = _dbContext
            .Users
            .SingleOrDefault(x => x.EMailAddress == registerUserDto.EMailAddress);

        if(user != null)
        {
            return BadRequest("User already exists");
        }

        user = new User()
        {
            EMailAddress = registerUserDto.EMailAddress,
            PasswordHash = registerUserDto.Password.ToHash()
        };

        _dbContext.Users.Add(user);

        _dbContext.SaveChanges();

        return Ok();
    }

    [HttpGet("course/registered")]
    public IActionResult GetRegisterCourses()
    {
        var user = _dbContext
            .Users
            .Include(x => x.CourseRegistrations)
            .SingleOrDefault(x => x.EMailAddress == AuthenticatedUserEMail);

        if (user == null)
        {
            return BadRequest("User not exists");
        }

        if (user.CoursesTaking.Any())
        {
            var courses = user
                .CoursesTaking
                .Select(course => new CourseDto()
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Descripition,
                    VideoUrl = course.Video.Url,
                    Instructor = course.Instructor.FullName
                });

            return Ok(courses);
        }
        else
            return Ok(Enumerable.Empty<CourseDto>());
	}
}