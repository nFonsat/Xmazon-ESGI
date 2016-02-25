using System;
using System.Collections.Generic;
using XmazonProject.WebService;
using System.Net;
using XmazonProject.Internet;
using Newtonsoft.Json;

namespace XmazonProject
{
	public class Store
	{
		public Store (String uid, String name)
		{
			this.uid = uid;
			this.name = name;
		}

		public String uid { private set; get; }

		public String name { private set; get; }

		public static List<Store> getAllStores(){

			List<Store> stores = new List<Store> ();

			//execution de la requete
			//Recuperation des données de la requete
			//Simulation de recuperation de données
			stores.Add(new Store("1", "Bricolage"));
			stores.Add(new Store("2", "Maison"));
			stores.Add(new Store("3", "Pret a porter"));
			stores.Add(new Store("4", "Jardinage"));
			stores.Add(new Store("5", "Informatique"));
			stores.Add(new Store("6", "Jeux jouets"));
			stores.Add(new Store("7", "Bricolage"));

			return stores;
		}


		public static List<Store> getLesStores(){
			StoreResponse mesStores = null;

			StoreWebService.Instance.GetList(callbackState => {
				if (callbackState.Exception != null) {
					WebException exception = callbackState.Exception;
					HttpWebResponse webResponse = (HttpWebResponse)exception.Response;
					Console.WriteLine ("GetSores Error : " + HttpXamarin.GetResponseText (webResponse.GetResponseStream ()));
				} 
				else {
					string jsonStr = HttpXamarin.GetResponseText (callbackState.ResponseStream);
					mesStores = JsonConvert.DeserializeObject<StoreResponse>(jsonStr);

					if(mesStores.code == 0){
						Console.WriteLine (jsonStr);

					}
					else{
						Console.WriteLine ("Probleme inattendu");
					}

				}
			});
			return mesStores.result;
		}


		public static List<Category> getNextCategories(String idBoutique, int limit, int offset){

			List<Category> lesCategories = new List<Category> ();

			//execution de la requete
			//Recuperation des données de la requete
			//Simulation de recuperation de données
			lesCategories.Add(new Category("1", "Bricolage"));
			lesCategories.Add(new Category("2", "Maison"));
			lesCategories.Add(new Category("3", "Pret a porter"));
			lesCategories.Add(new Category("4", "Jardinage"));
			lesCategories.Add(new Category("5", "Informatique"));
			lesCategories.Add(new Category("6", "Jeux jouets"));
			lesCategories.Add(new Category("7", "Bricolage"));

			return lesCategories;
		}
	}
}

