using CavinsStuff.Extentions;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CavinsStuff.Helpers
{
	/// <summary>
	/// Used as a simplifer for using http client
	/// </summary>
	public class ClientHelper : IDisposable
	{
		private HttpClient _client;
		public ClientHelper(HttpClient Client)
		{
			_client = Client;
		}

		public void Dispose()
		{
			_client.Dispose();
		}

		/// <summary>
		/// Gets a response from the URL provided and deserializes the results to the specified type.
		/// </summary>
		/// <typeparam name="T">Type to deserialize results to.</typeparam>
		/// <param name="url">URL appended to the base to get results</param>
		/// <returns></returns>
		public T Get<T>(string url) where T : class
		{
			// Get the response
			var response = _client.GetAsync(url).Result;

			// Handle response
			return response.HandleResponse<T>();
		}


		/// <summary>
		/// Posts to a url with no body
		/// </summary>
		/// <typeparam name="T">Expected return Type</typeparam>
		/// <param name="url">Url to post to.</param>
		public T PostToUrl<T>(string url) where T : class
		{
			// Get the response
			var response = _client.PostAsync(url, null).Result;

			// Handle response
			return response.HandleResponse<T>();
		}


		/// <summary>
		/// Posts to a url with a Json a body
		/// </summary>
		/// <typeparam name="T">Expected return Type</typeparam>
		/// <param name="url">Url to post to</param>
		/// <param name="requestObjectModel">Request Object Model to be converted into the Json body.</param>
		/// <returns>Type of T</returns>
		public T PostJson<T>(string url, object requestObjectModel) where T : class
		{
			//Add extention to url 
			var response = _client.PostAsJsonAsync(url, requestObjectModel).Result;

			// Handle response
			return response.HandleResponse<T>();
		}

		/// <summary>
		/// Appends string objects to url. Ignores null values
		/// </summary>
		/// <param name="url">Url to append values to.</param>
		/// <param name="urlParameters">String expected as Name=Value. Example: firstname=sam</param>
		/// <returns></returns>
		public string BuildUrl(string url, params string[] urlParameters)
		{
			var paramStr = "";
			if (urlParameters != null)
			{
				for (int i = 0; i < urlParameters.Length; i++)
				{
					var item = urlParameters[i];
					if (!string.IsNullOrWhiteSpace(item) && !item.EndsWith("="))
					{
						//Remove the unnessary symbols
						if (item.StartsWith("?") || item.StartsWith("&")) item = item.Remove(0, 1);

						//Prepend "?" to first valid parametere then prepend "&" to the rest
						if (!paramStr.StartsWith("?"))
							paramStr = $"?{item}";
						else
							paramStr += $"&{item}";
					}
				}
			}
			return $"{url}{paramStr}";
		}

		/// <summary>
		/// Adds the Header to the underling HttpClient
		/// </summary>
		/// <param name="name">Name of the header value</param>
		/// <param name="value">Value of the header</param>
		public bool TryAddWithoutValidation(string name, string value) => _client.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
	}
}