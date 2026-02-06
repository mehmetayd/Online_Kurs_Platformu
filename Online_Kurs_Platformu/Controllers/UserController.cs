using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineCourse.UI.Dto;
using OnlineCourse.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourse.UI.Controllers;

public class UserController : UIControllerBase
{
	public UserController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		: base(configuration, httpClientFactory)
	{
	}

	public async Task<IActionResult> Index(CancellationToken cancellationToken)
	{
		using var client = GetHttpClient();

		var response = await client.GetFromJsonAsync<List<CourseDto>>("user/course/registered", cancellationToken);

		var viewModel = response.Select(dto => new UserRegisteredCourseModel()
		{
			Id = dto.Id,
			Title = dto.Title,
			Description = dto.Description,
			VideoUrl = dto.VideoUrl,
			Instructor = dto.Instructor
		});

		return View(viewModel);
	}
}