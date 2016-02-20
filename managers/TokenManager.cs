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
			bool istoken = Application.Current.Properties.ContainsKey (ACCESS_TOKEN_APP);
			bool isRefresh = Application.Current.Properties.ContainsKey (ACCESS_TOKEN_APP);

			return istoken && isRefresh;
		}

		public bool ContainsUserAccessToken ()
		{

			bool istoken = Application.Current.Properties.ContainsKey (ACCESS_TOKEN_USER);
			bool isRefresh = Application.Current.Properties.ContainsKey (REFRESH_TOKEN_USER);

			return istoken && isRefresh;
		}

		public string GetRefreshToken (OAuthContext context)
		{
			string refreshToken = null;

			switch (context) {
			case OAuthContext.AppContext:
				refreshToken = (Application.Current.Properties.ContainsKey (REFRESH_TOKEN_APP)) ?
					(string) Application.Current.Properties [REFRESH_TOKEN_APP] : null;
				break;

			case OAuthContext.UserContext:
				refreshToken = (Application.Current.Properties.ContainsKey (REFRESH_TOKEN_USER)) ?
					(string) Application.Current.Properties [REFRESH_TOKEN_USER] : null;
				break;
			}

			return refreshToken;
		}

		public void StorageToken (AccessToken token, OAuthContext context)
		{
			switch (context) {
			case OAuthContext.AppContext:
				Application.Current.Properties [ACCESS_TOKEN_APP] = token.access_token;
				Application.Current.Properties [REFRESH_TOKEN_APP] = token.refresh_token;
				Application.Current.SavePropertiesAsync ();
				break;

			case OAuthContext.UserContext:
				Application.Current.Properties [ACCESS_TOKEN_USER] = token.access_token;
				Application.Current.Properties [REFRESH_TOKEN_USER] = token.refresh_token;
				Application.Current.SavePropertiesAsync ();
				break;
			}


		}

		public void DeleteToken (OAuthContext context)
		{
			switch (context) {
			case OAuthContext.AppContext:
				Application.Current.Properties.Remove (ACCESS_TOKEN_APP);
				Application.Current.Properties.Remove (REFRESH_TOKEN_APP);
				Application.Current.SavePropertiesAsync ();
				break;

			case OAuthContext.UserContext:
				Application.Current.Properties.Remove (ACCESS_TOKEN_USER);
				Application.Current.Properties.Remove (REFRESH_TOKEN_USER);
				Application.Current.SavePropertiesAsync ();
				break;
			}
		}
	}
}

