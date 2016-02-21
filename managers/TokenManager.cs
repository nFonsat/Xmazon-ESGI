using System;
using Xamarin.Forms;
using XmazonProject.Internet;
using XmazonProject.WebService;
using XmazonProject.Models;

namespace XmazonProject.Manager
{
	public class TokenManager
	{
		public static readonly string ACCESS_TOKEN_APP		= "ACCESS_TOKEN_APP";

		public static readonly string REFRESH_TOKEN_APP		= "REFRESH_TOKEN_APP";

		public static readonly string ACCESS_TOKEN_USER		= "ACCESS_TOKEN_USER";

		public static readonly string REFRESH_TOKEN_USER	= "REFRESH_TOKEN_USER";

		private static readonly TokenManager instance = new TokenManager();

		private TokenManager (){}

		public static TokenManager Instance
		{
			get 
			{
				return instance; 
			}
		}

		public bool ContainsAppAccessToken ()
		{
			bool isToken = App.Current.Properties.ContainsKey (ACCESS_TOKEN_APP) && 
				App.Current.Properties [ACCESS_TOKEN_APP] != null;
			bool isRefresh = App.Current.Properties.ContainsKey (REFRESH_TOKEN_APP) && 
				App.Current.Properties [REFRESH_TOKEN_APP] != null;

			return isToken && isRefresh;
		}

		public bool ContainsUserAccessToken ()
		{

			bool istoken = App.Current.Properties.ContainsKey (ACCESS_TOKEN_USER) && 
				App.Current.Properties [ACCESS_TOKEN_USER] != null;
			
			bool isRefresh = App.Current.Properties.ContainsKey (REFRESH_TOKEN_USER)&& 
				App.Current.Properties [REFRESH_TOKEN_USER] != null;

			return istoken && isRefresh;
		}

		public string GetRefreshToken (OAuthContext context)
		{
			string refreshToken = null;

			switch (context) {
			case OAuthContext.AppContext:
				refreshToken = (App.Current.Properties.ContainsKey (REFRESH_TOKEN_APP)) ?
					(string) App.Current.Properties [REFRESH_TOKEN_APP] : null;
				break;

			case OAuthContext.UserContext:
				refreshToken = (App.Current.Properties.ContainsKey (REFRESH_TOKEN_USER)) ?
					(string) App.Current.Properties [REFRESH_TOKEN_USER] : null;
				break;
			}

			return refreshToken;
		}

		public void StorageToken (AccessToken token, OAuthContext context)
		{
			switch (context) {
			case OAuthContext.AppContext:
				App.Current.Properties [ACCESS_TOKEN_APP] = token.access_token;
				App.Current.Properties [REFRESH_TOKEN_APP] = token.refresh_token;
				App.Current.SavePropertiesAsync ();
				break;

			case OAuthContext.UserContext:
				App.Current.Properties [ACCESS_TOKEN_USER] = token.access_token;
				App.Current.Properties [REFRESH_TOKEN_USER] = token.refresh_token;
				App.Current.SavePropertiesAsync ();
				break;
			}


		}

		public void DeleteToken (OAuthContext context)
		{
			switch (context) {
			case OAuthContext.AppContext:
				App.Current.Properties.Remove (ACCESS_TOKEN_APP);
				App.Current.Properties.Remove (REFRESH_TOKEN_APP);
				App.Current.SavePropertiesAsync ();
				break;

			case OAuthContext.UserContext:
				App.Current.Properties.Remove (ACCESS_TOKEN_USER);
				App.Current.Properties.Remove (REFRESH_TOKEN_USER);
				App.Current.SavePropertiesAsync ();
				break;
			}
		}
	}
}

