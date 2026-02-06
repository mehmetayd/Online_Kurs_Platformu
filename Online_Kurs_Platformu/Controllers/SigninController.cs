using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineCourse.UI.Dto;
using OnlineCourse.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourse.UI.Controllers;

public class SigninController : UIControllerBase
{
	public SigninController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		: base(configuration, httpClientFactory)
	{
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> SigninUser(SigninUserModel signinUserModel, CancellationToken cancellationToken)
	{
		using var client = GetHttpClient();

		var authenticateUserDto = new AuthenticateUserDto()
		{
			EMail = signinUserModel.EMailAddress,
			Password = signinUserModel.Password
		};

		var response = await client.PostAsJsonAsync("authenticate/login", authenticateUserDto, cancellationToken);

		if (response.IsSuccessStatusCode)
		{
			var token = await response.Content.ReadFromJsonAsync<TokenDto>(cancellationToken);

			HttpContext.Session.SetString("accessToken", token.AccessToken);
			HttpContext.Session.SetString("tokenType", token.TokenType);

			return RedirectToAction("Index", "User");
		}

		ViewBag["Error"] = "Invalid username or password";

		return View();
	}
}
