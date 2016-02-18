using System;

using Xamarin.Forms;
using XmazonProject.Internet;

namespace XmazonProject
{
	public class App : Application
	{
		public App ()
		{
			OAuth2Manager manager = OAuth2Manager.Instance;
			if (!manager.ContainsAppAccessToken ()) {
				OAuth2Manager.Instance.OAuth2ClientCredentials ();
			}
			
			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};
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

