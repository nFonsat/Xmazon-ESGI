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
	public partial class CategoryListView : ContentPage
	{
		Store myStore = null;

		public CategoryListView ()
		{
		}

		public CategoryListView (Store store)
		{
			this.myStore = store;
			this.Title = store.name;

			CategoryWebService.Instance.GetList(myStore.uid, callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine ("GetSores Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
				} 
				else {

					string jsonStr = HttpXamarin.GetResponseText (callbackState.ResponseStream);
					CategoryResponse mesCategory = JsonConvert.DeserializeObject<CategoryResponse>(jsonStr);

					if(mesCategory.code == 0){
						Device.BeginInvokeOnMainThread(() =>  {
							this.list.ItemsSource = mesCategory.result;
						});

						this.list.ItemSelected += (sender, e) => {
							if (e.SelectedItem == null) {
								return;
							}

							Category monCat = e.SelectedItem as Category;
							goToProductListView(monCat);
						};
					}
					else{
						Console.WriteLine ("Probleme inattendu");
					}
				}
			});

			InitializeComponent ();
		}



		public void goToProductListView(Category myCat)
		{
			Device.BeginInvokeOnMainThread(() =>  {
//				this.Navigation.PushAsync (new ProductListView(myCat));
			});
		}
	}
}
