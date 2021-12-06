
using RestSharp.Dependencies.Services;

using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace RestSharp.Dependencies.Tests
{
	public class GetRequestTest
	{
		[Theory]
		[InlineData("1")]
		[InlineData("2")]
		[InlineData("3")]
		public async Task GetPostsAndReturnOk(string value)
		{
			var response = await new RequestService
			{
				URL = "http://jsonplaceholder.typicode.com",
				URN = $"posts/{value}",
				Method = Method.GET
			}
			.SendAsync();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Theory]
		[InlineData("345k34l5k")]
		[InlineData("23k4l2k34")]
		[InlineData("fasd8f9asd8")]
		public async Task GetPostsAndReturnNotFound(string value)
		{
			var response = await new RequestService
			{
				URL = "http://jsonplaceholder.typicode.com",
				URN = $"posts/{value}",
				Method = Method.GET
			}
			.SendAsync();

			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}