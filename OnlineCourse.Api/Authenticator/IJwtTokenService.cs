namespace OnlineCourse.Api.Authenticator;

public interface IJwtTokenService
{
    string CreateToken(string userId, string email);
}
