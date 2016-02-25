using System;
using System.Collections.Generic;
using XmazonProject.Models;

using Xamarin.Forms;

namespace XmazonProject
{
	public partial class CategoryListView : ContentPage
	{
		Category myCategory = null;

		public CategoryListView ()
		{
		}

		public CategoryListView (Category cat)
		{
			this.myCategory = cat;
		
			this.Title = cat.name;

		
		}
	}
}
