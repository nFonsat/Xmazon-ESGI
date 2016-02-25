using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XmazonProject.Internet;
using System.Net;
using Newtonsoft.Json;
using XmazonProject.WebService;
using XmazonProject.Models;
using XmazonProject.Manager;
using System.Threading.Tasks;

namespace XmazonProject
{
	public partial class LoginPage : BaseContentPage
	{
		private TokenManager tokenManager = TokenManager.Instance;

		public LoginPage ()
		{
			Title = "Sign in";
			InitializeComponent ();
		}

		void SignInAction(object sender, EventArgs ea)
		{
			string username = this.username.Text;
			string password = this.password.Text;

			if (!username.Equals("") || !password.Equals("")) {
				OAuth2Manager.Instance.OAuth2Password (username, password, callbackState => {
					if (callbackState.Exception != null) {
						WebException exception = callbackState.Exception;
						HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
						tokenManager.DeleteToken (OAuthContext.UserContext);
					}
					else {
						string accessTokenUserJson = HttpXamarin.GetResponseText (callbackState.ResponseStream);
						AccessToken token = JsonConvert.DeserializeObject<AccessToken>(accessTokenUserJson);
						tokenManager.StorageToken(token, OAuthContext.UserContext);
						goToHomePage ();
					}
				});
			}
		}

		private void goToHomePage ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				ReplaceRootAsync (new HomePage ());
			});
		}
	}
}

