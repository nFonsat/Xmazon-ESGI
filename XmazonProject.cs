using System;

using Xamarin.Forms;
using XmazonProject.Internet;
using XmazonProject.WebService;
using System.Net;
using XmazonProject.Manager;

namespace XmazonProject
{
	public class App : Application
	{
		public App ()
		{
			TokenManager manager = TokenManager.Instance;
			if (!manager.ContainsAppAccessToken ()) {
				OAuth2Manager.Instance.OAuth2ClientCredentials ();
			}

			MainPage = new NavigationPage (new SplashScreen ());
		}

		protected override void OnStart ()
		{
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

