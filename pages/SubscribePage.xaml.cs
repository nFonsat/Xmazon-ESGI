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
	public partial class SubscribePage : ContentPage
	{
		private TokenManager tokenManager = TokenManager.Instance;

		public SubscribePage ()
		{
			Title = "Subscribe";
			InitializeComponent ();
		}


		void SubscribeAction(object sender, EventArgs ea)
		{
			String email =  "";
			email = this.email.Text;

			String password =  "";
			password = this.password.Text;

			String firstname =  "";
			firstname = this.firstname.Text;

			String lastname =  "";
			lastname = this.lastname.Text;

			String birthdate =  "";
			birthdate = this.birthdate.Text;		

			if (!email.Equals ("") && !password.Equals ("") && !firstname.Equals ("") && !lastname.Equals ("") && !birthdate.Equals ("")) {
				AuthWebService.Instance.SubscribeUser (email, password, firstname, lastname, birthdate, callbackState => {
					if (callbackState.Exception != null) {
						WebException exception = callbackState.Exception;
						HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
						tokenManager.DeleteToken (OAuthContext.UserContext);
					} else {
						string accessTokenUserJson = HttpXamarin.GetResponseText (callbackState.ResponseStream);
						AccessToken token = JsonConvert.DeserializeObject<AccessToken> (accessTokenUserJson);
						tokenManager.StorageToken (token, OAuthContext.UserContext);
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

		private void goToLoginPage ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				ReplaceRootAsync (new LoginPage ());
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

