using System;
using System.Collections.Generic;
using XmazonProject.Models;
using Xamarin.Forms;

namespace XmazonProject
{
	public partial class ProductListView : ContentPage
	{
		public ProductListView ()
		{
			//InitializeComponent ();

			this.Title = "Categorie X";
			Category myCat = new Category ("az", "az");

			// Define some data.
			List<Product> products = null;

			// Create the ListView.
			ListView listView = new ListView
			{
				// Source of data items.
				ItemsSource = products,

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
									//boxView,
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

