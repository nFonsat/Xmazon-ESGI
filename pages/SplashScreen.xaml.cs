using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XmazonProject.Manager;
using XmazonProject.WebService;
using System.Net;
using XmazonProject.Internet;

namespace XmazonProject
{
	public partial class SplashScreen : BaseContentPage
	{
		public SplashScreen ()
		{
			TokenManager manager = TokenManager.Instance;
			if (manager.ContainsUserAccessToken ()) {
				UserWebService.Instance.GetUser (callbackState => {
					if (callbackState.Exception != null) {
						WebException exception = callbackState.Exception;
						HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
						Console.WriteLine ("GetUser Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
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

		private void goToHomePage ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				ReplaceRootAsync (new HomePage ());
			});
		}

		private void laun
	}
}

