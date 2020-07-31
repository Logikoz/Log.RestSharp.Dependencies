using RestSharp.Dependencies.Services;

using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace RestSharp.Dependencies.Tests
{
	public class PostRequestTest
	{
		[Theory]
		[InlineData("{ \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1 }")]
		[InlineData("{ \"title\": \"foo2\", \"body\": \"bar2\", \"userId\": 2 }")]
		public async Task SendPostAndReturnData(string json)
		{
			var response = await new RequestService
			{
				URL = "http://jsonplaceholder.typicode.com",
				URN = $"posts",
				Method = Method.POST,
				Body = json
			}.ExecuteTaskAsync();

			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}
	}
}