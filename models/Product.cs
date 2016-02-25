using System;

namespace XmazonProject
{
	public class Product
	{
		public Product(String uid, String name)
		{
			this.uid = uid;
			this.name = name;
		}

		public String uid { private set; get; }

		public String name { private set; get; }

		public float price { private set; get; }

		public bool available { private set; get; }
	}
}

