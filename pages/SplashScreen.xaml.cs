using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XmazonProject.Manager;
using XmazonProject.WebService;
using System.Net;
using XmazonProject.Internet;

namespace XmazonProject
{
	public partial class SplashScreen : ContentPage
	{
		public SplashScreen ()
		{
			TokenManager manager = TokenManager.Instance;
			if (manager.ContainsUserAccessToken ()) {
				UserWebService.Instance.GetUser (callbackState => {
					if (callbackState.Exception != null) {
						goToLoginPage ();
					} 
					else {
						goToHomePage ();
					}
				});
			}
			
			InitializeComponent ();
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

