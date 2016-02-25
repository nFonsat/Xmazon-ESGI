using System;
using System.Collections.Generic;

namespace XmazonProject
{
	public class Category
	{
		public Category(String uid, String name)
		{
			this.uid = uid;
			this.name = name;
			this.products = new List<Product> ();
		}

		public String uid { private set; get; }

		public String name { private set; get; }

		public List<Product> products { private set; get; }


		//		public static List<Category> getNextCategories(String idBoutique, int limit, int offset){
		//
		//			List<Category> lesCategories = new List<Category> ();
		//
		//			//execution de la requete
		//			//Recuperation des données de la requete
		//			//Simulation de recuperation de données
		//			lesCategories.Add(new Category("1", "Bricolage"));
		//			lesCategories.Add(new Category("2", "Maison"));
		//			lesCategories.Add(new Category("3", "Pret a porter"));
		//			lesCategories.Add(new Category("4", "Jardinage"));
		//			lesCategories.Add(new Category("5", "Informatique"));
		//			lesCategories.Add(new Category("6", "Jeux jouets"));
		//			lesCategories.Add(new Category("7", "Bricolage"));
		//
		//			return lesCategories;
		//		}


		public static List<Product> getNextProducts(String idCategory, int limit, int offset){

			List<Product> products = new List<Product> ();

			//execution de la requete
			//Recuperation des données de la requete
			//Simulation de recuperation de données
			products.Add(new Product("1", "PC"));
			products.Add(new Product("2", "PC portable"));
			products.Add(new Product("3", "Telephone"));
			products.Add(new Product("4", "Disques durs"));
			products.Add(new Product("5", "SSD"));
			products.Add(new Product("6", "Cartes memoires"));
			products.Add(new Product("7", "Blabla"));

			return products;
		}
	}
}

