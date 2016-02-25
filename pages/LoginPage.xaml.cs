using System;
using Xamarin.Forms;
using XmazonProject.Manager;
using XmazonProject.Internet;
using XmazonProject.Models;
using XmazonProject.WebService;
using Newtonsoft.Json;
using System.Net;

namespace XmazonProject
{
	public partial class LoginPage : ContentPage
	{
		private TokenManager tokenManager = TokenManager.Instance;

		public LoginPage ()
		{
			Title = "Sign in";
			InitializeComponent ();
		}

		void SubscribeAction(object sender, EventArgs ea){
			goToSubscribe ();
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

		private void goToSubscribe ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				ReplaceRootAsync (new SubscribePage ());
			});
		}
			
		private void ReplaceRootAsync(Page page)
		{
			NavigationPage navigation = new NavigationPage(page);
			App.Current.MainPage = navigation;
			this.Navigation.PopToRootAsync();
		}
	}
}

