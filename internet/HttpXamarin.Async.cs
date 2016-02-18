using System;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace XmazonProject.Internet
{
	public partial class HttpXamarin
	{
		protected Action<HttpWebRequestCallbackState> _ResponseCallback;
		protected object _State;

		protected virtual void BeginGetResponseCallback(IAsyncResult asyncResult)
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

		protected virtual void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
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

		public virtual void ExecuteAsync (
			Action<HttpWebRequestCallbackState> responseCallback, 
			object state = null)
		{
			_Request = CreateHttpWebRequest(Url, Method, ContentType);

			if ((Method.Equals ("POST") || Method.Equals ("PUT") || Method.Equals ("DELETE")) && PostParameters != null) {
				byte[] requestBytes = GetRequestBytes (PostParameters);
				_Request.ContentLength = requestBytes.Length;

				_Request.BeginGetRequestStream (BeginGetRequestStreamCallback,
					new HttpWebRequestAsyncState () {
						RequestBytes = requestBytes,
						HttpWebRequest = _Request,
						ResponseCallback = responseCallback,  
						State = state
					}
				);
			} else {
				_Request.BeginGetResponse(BeginGetResponseCallback,
					new HttpWebRequestAsyncState()
					{
						HttpWebRequest = _Request,
						ResponseCallback = responseCallback,
						State = state
					}
				);
			}
		}
	}
}

