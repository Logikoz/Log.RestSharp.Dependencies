using Dependencies.Services;

using RestSharp;

using System;
using System.Threading.Tasks;

namespace Dependencies.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			Tests();
		}

		private static async void Tests()
		{
			await SendRequestTaskAsync();

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				RequestService requestService = new RequestService
				{
					Method = Method.POST,
					URL = $"http://192.168.1.6:5000/api",
					URN = "auth/signup"
				};
				return await requestService.ExecuteTaskAsync();
			}
		}
	}
}
