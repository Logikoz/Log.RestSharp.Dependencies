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
        public bool ContainsParameter { private get; set; } = false;
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public bool IsAuthorizable { private get; set; } = false;
        public IAuthenticator Authenticator { private get; set; }
        public string URN { private get; set; }
        public string URL { private get; set; }
        public string UserAgent { private get; set; }
        public (string name, string value) Header { private get; set; } = ("Content-Type", "application/json");

        public async Task<IRestResponse> ExecuteTaskAsync()
        {
            var request = new RestRequest(Method);
            request.AddHeader(Header.name, Header.value);

            if (Body != null)
                request.AddJsonBody(Body);

            var client = new RestClient($"{URL}/{URN}{(ContainsParameter ? GetParameters() : string.Empty)}")
            {
                UserAgent = UserAgent
            };
            if (IsAuthorizable)
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