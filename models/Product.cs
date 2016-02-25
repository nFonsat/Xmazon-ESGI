using System;
using Newtonsoft.Json;
using System.Json;

namespace XmazonProject
{
	public class Product
	{
		public Product(String uid, String name, float price, bool available)
		{
			this.uid = uid;
			this.name = name;
			this.price = price;
			this.available = available;
		}

		public Product(String uid, String name)
		{
			this.uid = uid;
			this.name = name;
		}

		public String uid { private set; get; }

		public String name { private set; get; }

		public float price { private set; get; }

		public bool available { private set; get; }

		public static Product Deserialize (JsonValue jsonCategory)
		{
			string uid = jsonCategory ["uid"];
			string name = jsonCategory ["name"];
			float price = (float)jsonCategory ["price"];
			bool available = (bool)jsonCategory ["available"];

			return new Product (uid, name, price, available);
		}
	}
}

