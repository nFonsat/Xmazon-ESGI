﻿using System;
using System.Collections.Specialized;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace XmazonProject.Internet
{
	public sealed class OAuth2Manager
	{
		public enum OAuthContext {
			UserContext,
			AppContext
		}
		
		public const string ACCESS_TOKEN_APP 	= "ACCESS_TOKEN_APP";

		public const string REFRESH_TOKEN_APP 	= "REFRESH_TOKEN_APP";

		public const string ACCESS_TOKEN_USER 	= "ACCESS_TOKEN_USER";

		public const string REFRESH_TOKEN_USER 	= "REFRESH_TOKEN_USER";

		public const string OAUTH_URL 			= "http://xmazon.appspaces.fr/oauth/token";

		public const string CLIENT_ID 			= "455c23ee-8604-49bd-81e5-1dad664d06da";

		public const string CLIENT_SECRET 		= "e5293794fb214db14e8740abc69e01a2ffcb00ad";

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
					DeleteToken (OAuthContext.AppContext);
					Console.WriteLine ("OAuth2ClientCredentials Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream()));
				}
				else {
					string accessTokenAppJson = HttpXamarin.GetResponseText (callbackState.ResponseStream);
					AccessToken token = JsonConvert.DeserializeObject<AccessToken>(accessTokenAppJson);
					StorageToken (token, OAuthContext.AppContext);
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
					Console.WriteLine ("OAuth2Password Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream()));
					DeleteToken (OAuthContext.UserContext);
				}
				else {
					string accessTokenUserJson = HttpXamarin.GetResponseText (callbackState.ResponseStream);
					AccessToken token = JsonConvert.DeserializeObject<AccessToken>(accessTokenUserJson);
					StorageToken (token, OAuthContext.UserContext);
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

		public AccessToken OAuth2RefreshToken (string refreshToken, OAuthContext context)
		{
			NameValueCollection refreshTokenCollection = new NameValueCollection ();
			refreshTokenCollection.Set ("grant_type", "refresh_token");
			refreshTokenCollection.Set ("client_id", CLIENT_ID);
			refreshTokenCollection.Set ("client_secret", CLIENT_SECRET);
			refreshTokenCollection.Set ("refresh_token", refreshToken);

			HttpWebResponse webResponse = HttpXamarin.PostSync (OAUTH_URL, 
				refreshTokenCollection, 
				"application/x-www-form-urlencoded");

			string accessTokenJson = HttpXamarin.GetResponseText (webResponse.GetResponseStream ());
			AccessToken token = JsonConvert.DeserializeObject<AccessToken>(accessTokenJson);
			StorageToken (token, OAuthContext.AppContext);

			return token;
		}

		private void StorageToken (AccessToken token, OAuthContext context)
		{
			switch (context) {
			case OAuthContext.AppContext:
				Application.Current.Properties [ACCESS_TOKEN_APP] = token.access_token;
				Application.Current.Properties [REFRESH_TOKEN_APP] = token.refresh_token;
				break;

			case OAuthContext.UserContext:
				Application.Current.Properties [ACCESS_TOKEN_USER] = token.access_token;
				Application.Current.Properties [REFRESH_TOKEN_USER] = token.refresh_token;
				break;
			}
		}

		private void DeleteToken (OAuthContext context)
		{
			switch (context) {
			case OAuthContext.AppContext:
				Application.Current.Properties.Remove (ACCESS_TOKEN_APP);
				Application.Current.Properties.Remove (REFRESH_TOKEN_APP);
				break;

			case OAuthContext.UserContext:
				Application.Current.Properties.Remove (ACCESS_TOKEN_USER);
				Application.Current.Properties.Remove (REFRESH_TOKEN_USER);
				break;
			}
		}
	}
}

