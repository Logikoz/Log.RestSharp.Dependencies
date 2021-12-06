using RestSharp.Authenticators;
using RestSharp.Dependencies.Services.Interfaces;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharp.Dependencies.Services
{
	public class RequestService : IRequestService
	{
		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

		public Method Method { private get; set; }
		public object Body { private get; set; }

		/// <summary>
		/// Use: Parameters.Add(paramName, paramValue);
		/// </summary>
		public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

		public IAuthenticator Authenticator { private get; set; }

		/// <summary>
		/// <para>without '/' before.</para>
		/// <para>Example: images/test.png</para>
		/// <para>parameters from URI: <see href="images/test.png?widht=100&amp;weight=100"/></para>
		/// </summary>
		public string URN { private get; set; }

		/// <summary>
		/// Only the URL, without the protocol.
		/// <para>Example: https://logikoz.net</para>
		/// </summary>
		public string URL { private get; set; }

		public string UserAgent { private get; set; }

		/// <summary>
		/// Default values:
		/// <para>name: Content-Type</para>
		/// <para>value: application/json</para>
		/// </summary>
		public Dictionary<string, string> Headers { private get; set; } = new Dictionary<string, string> { { "Content-Type", "application/json" } };

		public async Task<IRestResponse> SendAsync()
		{
			var request = new RestRequest(Method);

			foreach (var header in Headers)
				request.AddHeader(header.Key, header.Value);

			if (Body != null)
				request.AddJsonBody(Body);

			var client = new RestClient($"{URL}/{URN}{(Parameters.Count > 0 ? GetParameters() : string.Empty)}")
			{
				UserAgent = UserAgent
			};

			if (Authenticator != default)
				client.Authenticator = Authenticator;

			return await client.ExecuteAsync(request, _cancellationToken.Token);
		}

		public void CancelRequest() => _cancellationToken.Cancel();

		private string GetParameters()
		{
			var parameters = "?";

			foreach (var param in Parameters)
				parameters += $"{param.Key}={param.Value}{(Parameters.Count > 1 ? "&" : string.Empty)}";

			return parameters;
		}
	}
}