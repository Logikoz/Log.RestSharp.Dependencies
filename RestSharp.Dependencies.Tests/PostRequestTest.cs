using RestSharp.Dependencies.Services;

using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace RestSharp.Dependencies.Tests;

public class PostRequestTest
{
	[Theory]
	[InlineData("{ \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1 }")]
	[InlineData("{ \"title\": \"foo2\", \"body\": \"bar2\", \"userId\": 2 }")]
	public async Task SendPostAndReturnEqual201(string json)
	{
		var response = await new RequestService
		{
			URL = "http://jsonplaceholder.typicode.com",
			URN = $"posts",
			Method = Method.POST,
			Body = json
		}.SendAsync();

		Assert.Equal(HttpStatusCode.Created, response.StatusCode);
	}

	[Theory]
	[InlineData("{ \"title\": \"foo\", \"body\": \"bar, \"userId\": 1 }")]
	[InlineData("{ \"title\": \"body\": \"bar2\", \"userId\": 2 }")]
	[InlineData("l43k5l34k5l34k5l34")]
	[InlineData("------------------")]
	public async Task SendPostAndReturnEqual500(string json)
	{
		var response = await new RequestService
		{
			URL = "http://jsonplaceholder.typicode.com",
			URN = $"posts",
			Method = Method.POST,
			Body = json
		}.SendAsync();

		Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
	}
}