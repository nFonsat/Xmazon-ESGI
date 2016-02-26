using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XmazonProject
{
	public partial class ProductView : ContentPage
	{
		Product monProduit = null;

		public ProductView ()
		{
			initComponent ();
		}

		public ProductView(Product myPrd)
		{
			monProduit = myPrd;

			initComponent ();


			this.price.Text = String.Format ("Prix du produit : {0}€", monProduit.price);
			this.available.Text = (monProduit.available) ? "Disponible" : "Indisponible";
		}


		private void initComponent ()
		{
			Title = monProduit.name;
			InitializeComponent ();
		}

	}
}

