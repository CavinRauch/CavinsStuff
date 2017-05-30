using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CavinsStuff.Extentions
{
	/// <summary>
	/// Contains all client helper extentions.
	/// </summary>
	public static class ClientHelperExtenstions
	{
		/// <summary>
		/// Handles the response from the server
		/// </summary>
		public static T HandleResponse<T>(this HttpResponseMessage response) where T : class
		{
			// Handle response
			switch (response.StatusCode)
			{
				//Get Results
				case System.Net.HttpStatusCode.OK:
					return response.Content.ReadAsAsync<T>().Result;

				//Handle exception
				case System.Net.HttpStatusCode.InternalServerError:
					//Check that media type can be read from
					if (response.Content.Headers.ContentType.MediaType != "text/html")
					{
						Exception error = response.Content.ReadAsAsync<Exception>().Result;
						throw error;
					}
					else
						goto default;

				//default
				default:
					throw new ClientHelperException(response.ReasonPhrase, response);
			}
		}
	}

	public class ClientHelperException : Exception
	{
		public HttpResponseMessage Response { get; private set; }

		public ClientHelperException(string message, HttpResponseMessage response) : base(message)
		{
			Response = response;
		}
	}
}
