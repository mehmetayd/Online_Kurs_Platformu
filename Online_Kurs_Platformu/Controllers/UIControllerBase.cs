using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OnlineCourse.UI.Controllers;

public abstract class UIControllerBase : Controller
{
	private readonly IConfiguration _configuration;
	private readonly IHttpClientFactory _httpClientFactory;

	public UIControllerBase(IConfiguration configuration, IHttpClientFactory httpClientFactory)
	{
		_configuration = configuration;
		_httpClientFactory = httpClientFactory;
	}

	public HttpClient GetHttpClient()
	{
		var client = _httpClientFactory.CreateClient();

		var accessToken = HttpContext.Session.GetString("accessToken");
		var tokenType = HttpContext.Session.GetString("tokenType");

		if (accessToken != null && tokenType != null)
		{
			client
				.DefaultRequestHeaders
				.Authorization = new AuthenticationHeaderValue
				(
					tokenType,
					accessToken
				);
		}

		var apiAddressConfiguration = _configuration.GetSection("ApiAddress");
		var uriBuilder = new UriBuilder()
		{
			Scheme = apiAddressConfiguration["Protocol"],
			Host = apiAddressConfiguration["DnsName"],
			Port = int.Parse(apiAddressConfiguration["Port"])
		};

		client.BaseAddress = uriBuilder.Uri;

		return client;
	}
}
