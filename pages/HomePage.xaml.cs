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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			Title = "Home";

			InitializeComponent ();
		}

		void StoreAction(object sender, EventArgs ea){
			gotoStoreList ();
		}

		void logoutAction(object sender, EventArgs ea){
			TokenManager.Instance.DeleteToken (OAuthContext.AppContext);
			TokenManager.Instance.DeleteToken (OAuthContext.UserContext);
			goToLoginPage ();
		}

		void gotoStoreList ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				this.Navigation.PushAsync(new StoreListView ());
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

