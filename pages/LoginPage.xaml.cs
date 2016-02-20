using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XmazonProject.Internet;
using System.Net;
using Newtonsoft.Json;
using XmazonProject.WebService;
using XmazonProject.Models;

namespace XmazonProject
{
	public partial class LoginPage : ContentPage
	{
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
						Console.WriteLine (webResponse.StatusCode);
						Console.WriteLine ("OAuth2Password : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream()));
					}
					else {
						string accessTokenUserJson = HttpXamarin.GetResponseText (callbackState.ResponseStream);
						AccessToken token = JsonConvert.DeserializeObject<AccessToken>(accessTokenUserJson);
						Console.WriteLine ("OAuth2Password : " + token);
					}
				});
			}
		}
	}
}

