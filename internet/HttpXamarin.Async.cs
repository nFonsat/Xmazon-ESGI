using System;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace XmazonProject.Internet
{
	public partial class HttpXamarin
	{
		static void BeginGetResponseCallback(IAsyncResult asyncResult)
		{
			WebResponse webResponse = null;
			Stream responseStream = null;
			HttpWebRequestAsyncState asyncState = null;
			HttpWebRequestCallbackState webRequestCallbackState;

			try
			{
				asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
				webResponse = asyncState.HttpWebRequest.EndGetResponse(asyncResult);
				responseStream = webResponse.GetResponseStream();
				webRequestCallbackState = new HttpWebRequestCallbackState(responseStream, asyncState.State);
				asyncState.ResponseCallback(webRequestCallbackState);
				responseStream.Close();
				responseStream = null;
				webResponse.Close();
				webResponse = null;
			}
			catch (WebException ex)
			{
				if (asyncState != null)
					asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
				else
					throw;
			}
			finally
			{
				if (responseStream != null)
					responseStream.Close();
				if (webResponse != null)
					webResponse.Close();
			}
		}

		static void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
		{
			Stream requestStream = null;
			HttpWebRequestAsyncState asyncState = null;
			try
			{
				asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
				requestStream = asyncState.HttpWebRequest.EndGetRequestStream(asyncResult);
				requestStream.Write(asyncState.RequestBytes, 0, asyncState.RequestBytes.Length);
				requestStream.Close();
				asyncState.HttpWebRequest.BeginGetResponse(BeginGetResponseCallback,
					new HttpWebRequestAsyncState
					{
						HttpWebRequest = asyncState.HttpWebRequest,
						ResponseCallback = asyncState.ResponseCallback,
						State = asyncState.State
					});
			}
			catch (WebException ex)
			{
				if (asyncState != null)
					asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
				else
					throw;
			}
			finally
			{
				if (requestStream != null)
					requestStream.Close();
			}
		}

		static void ExecuteAsync (
			string method,
			string url, 
			Action<HttpWebRequestCallbackState> responseCallback, 
			object state = null,
			string contentType = "application/x-www-form-urlencoded",
			NameValueCollection postParameters = null)
		{
			HttpWebRequest httpWebRequest = CreateHttpWebRequest(url, method, contentType);

			if (method.Equals ("POST") || method.Equals ("PUT") || method.Equals ("DELETE")) {
				byte[] requestBytes = GetRequestBytes (postParameters);
				httpWebRequest.ContentLength = requestBytes.Length;

				httpWebRequest.BeginGetRequestStream (BeginGetRequestStreamCallback,
					new HttpWebRequestAsyncState () {
						RequestBytes = requestBytes,
						HttpWebRequest = httpWebRequest,
						ResponseCallback = responseCallback,  
						State = state
					}
				);
			} else {
				httpWebRequest.BeginGetResponse(BeginGetResponseCallback,
					new HttpWebRequestAsyncState()
					{
						HttpWebRequest = httpWebRequest,
						ResponseCallback = responseCallback,
						State = state
					}
				);
			}
		}

		public static void GetAsync(string url, 
			Action<HttpWebRequestCallbackState> responseCallback,
			string contentType = "application/x-www-form-urlencoded",
			object state = null)
		{
			ExecuteAsync ("GET",
				url,
				responseCallback,
				state,
				contentType
			);
		}

		public static void PostAsync (string url, 
			NameValueCollection postParameters,
			Action<HttpWebRequestCallbackState> responseCallback,
			string contentType = "application/x-www-form-urlencoded",
			object state = null)
		{
			ExecuteAsync ("POST",
				url,
				responseCallback,
				state,
				contentType,
				postParameters
			);
		}

		public static void PutAsync(string url, 
			NameValueCollection postParameters,
			Action<HttpWebRequestCallbackState> responseCallback, 
			string contentType = "application/x-www-form-urlencoded", 
			object state = null)
		{
			ExecuteAsync ("PUT",
				url,
				responseCallback,
				state,
				contentType,
				postParameters
			);
		}

		public static void DeleteAsync(string url, 
			NameValueCollection postParameters,
			Action<HttpWebRequestCallbackState> responseCallback,
			string contentType = "application/x-www-form-urlencoded",
			object state = null)
		{
			ExecuteAsync ("DELETE",
				url,
				responseCallback,
				state,
				contentType,
				postParameters
			);
		}
	}
}

