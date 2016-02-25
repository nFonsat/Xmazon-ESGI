using System;
using System.Collections.Generic;
using XmazonProject.Models;
using Xamarin.Forms;
using XmazonProject.WebService;
using System.Net;
using XmazonProject.Internet;

namespace XmazonProject
{
	public partial class ProductListView : ContentPage
	{
		private Category myCategory;

		public ProductListView ()
		{
			ProductWebService.Instance.GetList (callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine (HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
				}
				else {
					Console.WriteLine (HttpXamarin.GetResponseText (callbackState.ResponseStream));
				}
			});
			
			initComponent ();
		}

		public ProductListView (Category category)
		{
			myCategory = category;

			ProductWebService.Instance.GetList (callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine (webResponse.GetResponseStream ());
				}
				else {
					Console.WriteLine (callbackState.ResponseStream);
				}
			}, myCategory.uid);
			
			initComponent ();
		}

		private void initComponent ()
		{
			Title = "Liste de produit";
			InitializeComponent ();
		}
	}
}

