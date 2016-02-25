using System;

using Xamarin.Forms;

namespace XmazonProject
{
	public class BaseContentPage : ContentPage
	{
		public BaseContentPage ()
		{
			
		}

		public void ReplaceRootAsync(Page page)
		{
			NavigationPage navigation = new NavigationPage(page);
			App.Current.MainPage = navigation;
			this.Navigation.PopToRootAsync();
		}
	}
}


