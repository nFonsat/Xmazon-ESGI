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
					storeResponse mesStores = JsonConvert.DeserializeObject<storeResponse>(jsonStr);

					if(mesStores.code == 0){
						Device.BeginInvokeOnMainThread(() =>  {
							this.list.ItemsSource = mesStores.result;
//							fillList(mesStores.result);
						});

					}
					else{
						Console.WriteLine ("Probleme inattendu");
					}
				}
			});
			InitializeComponent ();

		}


		public void fillList(List<Store> stores){

			// Create the ListView.
			ListView listView = new ListView
			{
				// Source of data items.
				ItemsSource = stores,

				// Define template for displaying each item.
				// (Argument of DataTemplate constructor is called for 
				//      each item; it must return a Cell derivative.)
				ItemTemplate = new DataTemplate(() =>
					{
						// Create views with bindings for displaying each property.
						Label nameLabel = new Label();
						nameLabel.SetBinding(Label.TextProperty, "name");

						// Return an assembled ViewCell.
						return new ViewCell
						{
							View = new StackLayout
							{
								Padding = new Thickness(20, 5),
								Orientation = StackOrientation.Horizontal,
								Children = 
								{
									new StackLayout
									{
										VerticalOptions = LayoutOptions.Center,
										Spacing = 0,
										Children = 
										{
											nameLabel
										}
										}
								}	
								}
						};
					})
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			// Build the page.
			this.Content = new StackLayout
			{
				Children = 
				{
					listView
				}
				};
		}


		public void GoToAboutSection(object sender, EventArgs args)
		{
			//this.Navigation.PushAsync (new AboutPage());
		}
	}
}

