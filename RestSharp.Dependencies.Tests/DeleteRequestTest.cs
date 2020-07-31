using RestSharp.Dependencies.Services;

using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace RestSharp.Dependencies.Tests
{
	public class DeleteRequestTest
	{
		[Theory]
		[InlineData("1")]
		[InlineData("2")]
		[InlineData("3")]
		[InlineData("4")]
		public async Task DeletePostAndReturnEqual200(string value)
		{
			var response = await new RequestService
			{
				URL = "http://jsonplaceholder.typicode.com",
				URN = $"posts/{value}",
				Method = Method.DELETE
			}.ExecuteTaskAsync();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}