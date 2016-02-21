using System;
using XmazonProject.Manager;
using XmazonProject.Models;
using System.Collections.Specialized;
using XmazonProject.Internet;
using System.Net;
using Newtonsoft.Json;

namespace XmazonProject.WebService
{
	public enum OAuthContext {
		UserContext,
		AppContext
	}

	public sealed class OAuth2Manager : BaseWebService
	{
		public const string BASE_OAUTH 			= "/oauth/token";

		public const string CLIENT_ID 			= "455c23ee-8604-49bd-81e5-1dad664d06da";

		public const string CLIENT_SECRET 		= "e5293794fb214db14e8740abc69e01a2ffcb00ad";

		private TokenManager tokenManager 		= TokenManager.Instance;

		private static readonly OAuth2Manager instance = new OAuth2Manager();

		private OAuth2Manager () : base(BASE_OAUTH){}

		public static OAuth2Manager Instance
		{
			get 
			{
				return instance; 
			}
		}

		public AccessToken OAuth2ClientCredentials ()
		{
			AccessToken token = null;
			NameValueCollection clientCredentialCollection = new NameValueCollection ();
			clientCredentialCollection.Set ("grant_type", "client_credentials");
			clientCredentialCollection.Set ("client_id", CLIENT_ID);
			clientCredentialCollection.Set ("client_secret", CLIENT_SECRET);

			HttpXamarin http = new HttpXamarin (BaseUrl, 
				"POST", 
				"application/x-www-form-urlencoded", 
				clientCredentialCollection);
			HttpWebResponse httpResponse = http.ExecuteSync ();

			string responseString = HttpXamarin.GetResponseText (httpResponse.GetResponseStream ());

			if (httpResponse.StatusCode == HttpStatusCode.OK) {
				string accessTokenAppJson = responseString;
				token = JsonConvert.DeserializeObject<AccessToken>(accessTokenAppJson);
				tokenManager.StorageToken (token, OAuthContext.AppContext);
				Console.WriteLine ("OAuth2ClientCredentials : " + token);
			} else {
				tokenManager.DeleteToken (OAuthContext.AppContext);
				Console.WriteLine ("OAuth2ClientCredentials Error : " + responseString);
			}

			return token;
		}

		public void OAuth2Password (string username, string password, Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection passwordCollection = new NameValueCollection ();
			passwordCollection.Set ("grant_type", "password");
			passwordCollection.Set ("client_id", CLIENT_ID);
			passwordCollection.Set ("client_secret", CLIENT_SECRET);
			passwordCollection.Set ("username", username);
			passwordCollection.Set ("password", password);

			HttpXamarin http = new HttpXamarin (BaseUrl, 
				"POST", 
				"application/x-www-form-urlencoded", 
				passwordCollection);
			http.ExecuteAsync (responseCallback);
		}

		public AccessToken OAuth2RefreshToken (OAuthContext context)
		{
			AccessToken token = null;
			string refreshToken = tokenManager.GetRefreshToken (context);

			if (refreshToken != null) {
				NameValueCollection refreshTokenCollection = new NameValueCollection ();
				refreshTokenCollection.Set ("grant_type", "refresh_token");
				refreshTokenCollection.Set ("client_id", CLIENT_ID);
				refreshTokenCollection.Set ("client_secret", CLIENT_SECRET);
				refreshTokenCollection.Set ("refresh_token", refreshToken);

				HttpXamarin http = new HttpXamarin (BaseUrl, 
					"POST", 
					"application/x-www-form-urlencoded", 
					refreshTokenCollection);
				HttpWebResponse webResponse = http.ExecuteSync ();

				string responseString = HttpXamarin.GetResponseText (webResponse.GetResponseStream ());

				Console.WriteLine ("OAuth2RefreshToken : " + responseString);

				if (webResponse.StatusCode == HttpStatusCode.OK) {
					token = JsonConvert.DeserializeObject<AccessToken>(responseString);
					tokenManager.StorageToken (token, context);
					Console.WriteLine ("OAuth2RefreshToken : " + token);
				} else {
					tokenManager.DeleteToken (context);
				}
			}

			return token;
		}
	}
}

