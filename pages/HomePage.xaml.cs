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

			gotoStoreList ();
			InitializeComponent ();
		}

		private void goToLoginPage ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				this.Navigation.PushAsync(new LoginPage ());
			});
		}


		private void gotoStoreList ()
		{
			Device.BeginInvokeOnMainThread(() =>  {
				this.Navigation.PushAsync(new StoreListView ());
			});
		}
	}
}

