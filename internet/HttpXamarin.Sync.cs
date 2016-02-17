using System;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace XmazonProject.Internet
{
	public partial class HttpXamarin
	{
		static HttpWebResponse ExecuteSync (
			string method,
			string url,
			string contentType = "application/x-www-form-urlencoded",
			NameValueCollection postParameters = null)
		{
			HttpWebResponse webResponse = null;
			Stream requestStream = null;

			HttpWebRequest httpWebRequest = CreateHttpWebRequest(url, method, contentType);

			try 
			{
				if (method.Equals ("POST") || method.Equals ("PUT") || method.Equals ("DELETE")) {
					byte[] requestBytes = GetRequestBytes (postParameters);
					httpWebRequest.ContentLength = requestBytes.Length;
					requestStream = httpWebRequest.GetRequestStream ();
					requestStream.Write (requestBytes, 0, requestBytes.Length);
					requestStream.Close ();
					requestStream = null;
				}

				webResponse = (HttpWebResponse)httpWebRequest.GetResponse ();
			}
			catch (WebException exception) 
			{
				webResponse = (HttpWebResponse)exception.Response;
			}
			finally
			{
				if (requestStream != null) {
					requestStream.Close ();
					requestStream = null;
				}
			}

			return webResponse;
		}

		public static HttpWebResponse GetSync (string url, 
			string contentType = "application/x-www-form-urlencoded")
		{
			return ExecuteSync ("GET",
				url,
				contentType
			);
		}


		public static HttpWebResponse PostSync (string url, 
			NameValueCollection postParameters,
			string contentType = "application/x-www-form-urlencoded")
		{
			return ExecuteSync ("POST",
				url,
				contentType,
				postParameters
			);
		}


		public static HttpWebResponse PutSync (string url, 
			NameValueCollection postParameters,
			string contentType = "application/x-www-form-urlencoded")
		{
			return ExecuteSync ("PUT",
				url,
				contentType,
				postParameters
			);
		}


		public static HttpWebResponse DeleteSync (string url, 
			NameValueCollection postParameters,
			string contentType = "application/x-www-form-urlencoded")
		{
			return ExecuteSync ("DELETE",
				url,
				contentType,
				postParameters
			);
		}
	}
}

