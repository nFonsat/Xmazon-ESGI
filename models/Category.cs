using System;

namespace XmazonProject
{
	public class Category
	{
		public Category(String uid, String name)
		{
			this.uid = uid;
			this.name = name;
		}

		public String uid { private set; get; }

		public String name { private set; get; }

	}
}

