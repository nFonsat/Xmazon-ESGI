using System;
using System.Net;
using System.IO;
using XmazonProject.Models;
using XmazonProject.WebService;


namespace XmazonProject.Internet
{
	public partial class OAuthHttpXamarin : HttpXamarin
	{
		protected override void BeginGetResponseCallback(IAsyncResult asyncResult)
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
				HttpWebResponse response = (HttpWebResponse)ex.Response;
				Console.WriteLine (GetResponseText (response.GetResponseStream ()));
				if (response.StatusCode == HttpStatusCode.Unauthorized && _UseRefreshToken) {
					AccessToken token = OAuth2Manager.Instance.OAuth2RefreshToken (Context);
					if (token != null) {
						SetCredentialHeader ();
						ExecuteAsync (_ResponseCallback, false, _State);
					}
				} else {
					throw ex;
				}
			}
			finally
			{
				if (responseStream != null)
					responseStream.Close();
				if (webResponse != null)
					webResponse.Close();
			}
		}

		protected override void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
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
				HttpWebResponse response = (HttpWebResponse)ex.Response;
				if (response.StatusCode == HttpStatusCode.Unauthorized && _UseRefreshToken) {
					AccessToken token = OAuth2Manager.Instance.OAuth2RefreshToken (Context);
					if (token != null) {
						SetCredentialHeader ();
						ExecuteAsync (_ResponseCallback, false, _State);
					}
				} else {
					Console.WriteLine (GetResponseText (response.GetResponseStream ()));
					throw ex;
				}
			}
			finally
			{
				if (requestStream != null)
					requestStream.Close();
			}
		}

		public virtual void ExecuteAsync (
			Action<HttpWebRequestCallbackState> responseCallback,
			bool useRefreshToken = true,
			object state = null)
		{
			_UseRefreshToken = useRefreshToken;
			_ResponseCallback = responseCallback;
			_State = state;

			_Request = CreateHttpWebRequest(Url, Method, ContentType);

			SetCredentialHeader ();

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

