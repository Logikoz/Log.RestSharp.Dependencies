using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dependencies.Services
{
    public class RequestService
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
        /// <para>Result by joining the protocol, url and parameters: <see href="https://Logikoz.net/images/test.png?widht=100&amp;weight=100"/></para>
        /// </summary>
        public string URN { private get; set; }

        /// <summary>
        /// Only the URL, without the protocol.
        /// <para>Example: logikoz.net</para>
        /// </summary>
        public string URL { private get; set; }

        public string UserAgent { private get; set; }

        /// <summary>
        /// Default values:
        /// <para>name: Content-Type</para>
        /// <para>value: application/json</para>
        /// </summary>
        public (string name, string value) Header { private get; set; } = ("Content-Type", "application/json");

        public async Task<IRestResponse> ExecuteTaskAsync()
        {
            var request = new RestRequest(Method);
            request.AddHeader(Header.name, Header.value);

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
            string parameters = "?";
            Parameters.ToList().ForEach(param => parameters += $"{param.Key}={param.Value}{(Parameters.Count > 1 ? "&" : string.Empty)}");
            return parameters;
        }
    }
}