using System.Threading.Tasks;

namespace RestSharp.Dependencies.Services.Interfaces
{
	public interface IRequestService
	{
		/// <summary>
		/// Send a http or https request.
		/// </summary>
		/// <returns><see cref="IRestResponse"/> from request</returns>
		Task<IRestResponse> SendAsync();

		/// <summary>
		/// Cancel the last request
		/// </summary>
		void CancelRequest();
	}
}