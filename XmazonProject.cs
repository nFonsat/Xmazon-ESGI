using System;

using Xamarin.Forms;
using XmazonProject.Internet;
using XmazonProject.WebService;
using System.Net;

namespace XmazonProject
{
	public class App : Application
	{
		public App ()
		{
			OAuth2Manager manager = OAuth2Manager.Instance;
			if (!manager.ContainsAppAccessToken ()) {
				manager.OAuth2ClientCredentials ();
			}

			if (!manager.ContainsUserAccessToken ()) {
				MainPage = new NavigationPage (new LoginPage ());
			} 
			else {
				UserWebService.Instance.GetUser (callbackState => {
					if (callbackState.Exception != null) {
						WebException exception = callbackState.Exception;
						HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
						Console.WriteLine (webResponse.StatusCode);
						Console.WriteLine ("GetUser Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
						MainPage = new NavigationPage (new LoginPage ());
					} else {
						MainPage = new NavigationPage (new HomePage ());
					}
				});
			}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

