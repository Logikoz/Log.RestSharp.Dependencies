using RestSharp.Dependencies.Services;

using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace RestSharp.Dependencies.Tests;

public class PutRequestTest
{
	[Theory]
	[InlineData("{ \"id\": 1, \"title\": \"fooPut\", \"body\": \"barPut\", \"userId\": 3 }")]
	[InlineData("{ \"id\": 2, \"title\": \"foo2Put\", \"body\": \"bar2Put\", \"userId\": 4 }")]
	public async Task SendPostAndReturnEqual201(string json)
	{
		var response = await new RequestService
		{
			URL = "http://jsonplaceholder.typicode.com",
			URN = $"posts/1",
			Method = Method.PUT,
			Body = json
		}.SendAsync();

		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Theory]
	[InlineData("{ \"id\": 1, \"title\": \"fooPut\", \"bod \"userId\": 3 }")]
	[InlineData("{ \"id\": foo2Put\", \"body\": \"bar2Put\", \"userId\": 4 }")]
	[InlineData("alskdlas")]
	[InlineData("")]
	public async Task SendPostAndReturnEqual500(string json)
	{
		var response = await new RequestService
		{
			URL = "http://jsonplaceholder.typicode.com",
			URN = $"posts/1",
			Method = Method.PUT,
			Body = json
		}.SendAsync();

		Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
	}
}
