using System;
using System.Net;
using System.IO;
using XmazonProject.Models;
using XmazonProject.WebService;

namespace XmazonProject.Internet
{
	public partial class OAuthHttpXamarin : HttpXamarin
	{
		public virtual HttpWebResponse ExecuteSync (bool useRefreshToken = true)
		{
			_UseRefreshToken = useRefreshToken;
			HttpWebResponse webResponse = null;
			Stream requestStream = null;

			SetCredentialHeader ();

			_Request = CreateHttpWebRequest(Url, Method, ContentType);

			try 
			{
				if ((Method.Equals ("POST") || Method.Equals ("PUT") || Method.Equals ("DELETE")) && PostParameters != null) {
					byte[] requestBytes = GetRequestBytes (PostParameters);
					_Request.ContentLength = requestBytes.Length;
					requestStream = _Request.GetRequestStream ();
					requestStream.Write (requestBytes, 0, requestBytes.Length);
					requestStream.Close ();
					requestStream = null;
				}

				webResponse = (HttpWebResponse)_Request.GetResponse ();
			}
			catch (WebException exception) 
			{
				webResponse = (HttpWebResponse)exception.Response;
				if (webResponse.StatusCode == HttpStatusCode.Unauthorized && _UseRefreshToken) {
					AccessToken token = OAuth2Manager.Instance.OAuth2RefreshToken (Context);
					if (token != null) {
						SetCredentialHeader ();
						webResponse = ExecuteSync (false);
					}
				} else {
					Console.WriteLine (GetResponseText (webResponse.GetResponseStream ()));
				}
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
	}
}

