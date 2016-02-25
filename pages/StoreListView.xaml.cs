using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
	public partial class StoreListView : ContentPage
	{
		public StoreListView ()
		{
			this.Title = "Nos boutiques";

			StoreWebService.Instance.GetList(callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine ("GetSores Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
				} 
				else {

					string jsonStr = HttpXamarin.GetResponseText (callbackState.ResponseStream);
					StoreResponse mesStores = JsonConvert.DeserializeObject<StoreResponse>(jsonStr);

					if(mesStores.code == 0){
						Device.BeginInvokeOnMainThread(() =>  {
							this.list.ItemsSource = mesStores.result;
						});

						this.list.ItemSelected += (sender, e) => {
							if (e.SelectedItem == null) {
								return;
							}
							Store monStore = e.SelectedItem as Store;

							goToCategoryListView(monStore);
						};
					}
					else{
						Console.WriteLine ("Probleme inattendu");
					}
				}
			});

			InitializeComponent ();
		}


		public void goToCategoryListView(Store monStore)
		{
			Device.BeginInvokeOnMainThread(() =>  {
				this.Navigation.PushAsync (new CategoryListView(monStore));
			});
		}
	}
}

