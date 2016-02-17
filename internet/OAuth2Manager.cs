using System;
using System.Collections.Specialized;
using System.Net;

namespace XmazonProject.Internet
{
	public sealed class OAuth2Manager
	{
		public static readonly string OAUTH_URL = "http://xmazon.appspaces.fr/oauth/token";

		public static readonly string CLIENT_ID = "455c23ee-8604-49bd-81e5-1dad664d06da";

		public static readonly string CLIENT_SECRET = "e5293794fb214db14e8740abc69e01a2ffcb00ad";

		private static readonly OAuth2Manager instance = new OAuth2Manager();

		private OAuth2Manager (){}

		public static OAuth2Manager Instance
		{
			get 
			{
				return instance; 
			}
		}

		public void OAuth2ClientCredentials ()
		{
			Action<HttpWebRequestCallbackState> responseCallback = callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine (webResponse.StatusCode);
					Console.WriteLine ("Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream()));
				}
				else {
					Console.WriteLine ("CredentialCollection : " + HttpXamarin.GetResponseText (callbackState.ResponseStream));
				}
			};

			NameValueCollection clientCredentialCollection = new NameValueCollection ();
			clientCredentialCollection.Set ("grant_type", "client_credentials");
			clientCredentialCollection.Set ("client_id", CLIENT_ID);
			clientCredentialCollection.Set ("client_secret", CLIENT_SECRET);

			HttpXamarin.PostAsync (
				OAUTH_URL,
				clientCredentialCollection,
				responseCallback,
				"application/x-www-form-urlencoded"
			);
		}

		public void OAuth2Password (string username, string password)
		{
			Action<HttpWebRequestCallbackState> responseCallback = callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine (webResponse.StatusCode);
					Console.WriteLine ("Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream()));
				}
				else {
					Console.WriteLine ("Password : " + HttpXamarin.GetResponseText (callbackState.ResponseStream));
				}
			};

			NameValueCollection passwordCollection = new NameValueCollection ();
			passwordCollection.Set ("grant_type", "password");
			passwordCollection.Set ("client_id", CLIENT_ID);
			passwordCollection.Set ("client_secret", CLIENT_SECRET);
			passwordCollection.Set ("username", username);
			passwordCollection.Set ("password", password);

			HttpXamarin.PostAsync (
				OAUTH_URL,
				passwordCollection,
				responseCallback,
				"application/x-www-form-urlencoded"
			);
		}

		public void OAuth2RefreshToken (string refreshToken, string context)
		{
			NameValueCollection refreshTokenCollection = new NameValueCollection ();
			refreshTokenCollection.Set ("grant_type", "refresh_token");
			refreshTokenCollection.Set ("client_id", CLIENT_ID);
			refreshTokenCollection.Set ("client_secret", CLIENT_SECRET);
			refreshTokenCollection.Set ("refresh_token", refreshToken);

			HttpWebResponse webResponse = HttpXamarin.PostSync (OAUTH_URL, refreshTokenCollection, "application/x-www-form-urlencoded");
			Console.WriteLine ("refreshTokenCollection : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream()));
		}
	}
}

