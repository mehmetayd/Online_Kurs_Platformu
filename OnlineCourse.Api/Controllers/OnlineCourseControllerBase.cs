using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Kurs_Platformu.Data;
using System.Security.Claims;

namespace OnlineCourse.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class OnlineCourseControllerBase: ControllerBase
    {
        public string AuthenticatedUserEMail => GetAuthenticatedUserEMail(User);

        private string GetAuthenticatedUserEMail(ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? user.FindFirst("sub")?.Value;

            _ = int.TryParse(userIdString, out var userId);

            var email = user.FindFirstValue(ClaimTypes.Email) 
                        ?? user.FindFirst("email")?.Value;

            return email ?? "";
        }
    }
}
