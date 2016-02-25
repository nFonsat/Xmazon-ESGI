using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XmazonProject.Internet;
using System.Net;
using Newtonsoft.Json;
using XmazonProject.WebService;
using XmazonProject.Models;
using XmazonProject.Manager;
using System.Json;


namespace XmazonProject
{
	public partial class ProductListView : ContentPage
	{
		private Category myCategory;

		public ProductListView ()
		{
			initComponent ();
		}

		public ProductListView (Category category)
		{
			myCategory = category;

			ProductWebService.Instance.GetList (callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine (HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
				}
				else {
					string jsonStr = HttpXamarin.GetResponseText (callbackState.ResponseStream);
					ProductResponse mesProduits = JsonConvert.DeserializeObject<ProductResponse>(jsonStr);

					if(mesProduits.code == 0){
						Device.BeginInvokeOnMainThread(() =>  {
							this.list.ItemsSource = mesProduits.result;
						});

						this.list.ItemSelected += (sender, e) => {
							if (e.SelectedItem == null) {
								return;
							}

							Product myPrd = e.SelectedItem as Product;
							goToProductView(myPrd);
						};
					}
					else{
						Console.WriteLine ("Probleme inattendu");
					}
				}
			}, myCategory.uid);

			initComponent ();
		}

		private void initComponent ()
		{
//			Title = "Liste de produit";
			Title = myCategory.name;
			InitializeComponent ();
		}

		private void goToProductView(Product myPrd){
		
		
		}
	}
}

