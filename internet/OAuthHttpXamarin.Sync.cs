using System;
using System.Net;
using System.IO;
using XmazonProject.Models;
using XmazonProject.WebService;

namespace XmazonProject.Internet
{
	public partial class OAuthHttpXamarin : HttpXamarin
	{
		public override HttpWebResponse ExecuteSync ()
		{
			HttpWebResponse webResponse = null;
			Stream requestStream = null;

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
				if (webResponse.StatusCode == HttpStatusCode.Unauthorized) {
					AccessToken token = OAuth2Manager.Instance.OAuth2RefreshToken (Context);
					if (token != null) {
						SetCredentialHeader ();
						webResponse = base.ExecuteSync ();
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

