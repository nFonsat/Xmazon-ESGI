using System;
using System.Net;
using Xamarin.Forms;
using System.Collections.Specialized;

namespace XmazonProject.Internet
{
	public partial class OAuthHttpXamarin : HttpXamarin
	{
		public OAuthContext Context  { get; private set; }
		
		public OAuthHttpXamarin (string url, string httpMethod, OAuthContext context) : base(url, httpMethod)
		{
			Context = context;
		}

		public OAuthHttpXamarin (string url, string httpMethod, string contentType, OAuthContext context) : base (url, httpMethod, contentType)
		{
			Context = context;
		}

		public OAuthHttpXamarin (string url, string httpMethod, string contentType, NameValueCollection postParameters, OAuthContext context) : base (url, httpMethod, contentType, postParameters)
		{
			Context = context;
		}

		private void SetCredentialHeader()
		{
			string accessToken;
			string credential;
			
			switch (Context) {
			case OAuthContext.AppContext:
				accessToken = (string) Application.Current.Properties [OAuth2Manager.ACCESS_TOKEN_APP];
				credential = string.Format("{0} {1}", "Bearer", accessToken);
				_Request.Headers[HttpRequestHeader.Authorization] = credential;
				break;

			case OAuthContext.UserContext:
				accessToken = (string) Application.Current.Properties [OAuth2Manager.ACCESS_TOKEN_USER];
				credential = string.Format("{0} {1}", "Bearer", accessToken);
				_Request.Headers[HttpRequestHeader.Authorization] = credential;
				break;
			}
		}
	}
}

