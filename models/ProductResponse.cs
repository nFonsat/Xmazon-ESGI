using System;
using System.Collections.Generic;

namespace XmazonProject
{
	public class ProductResponse
	{
		public ProductResponse ()
		{
		}

		public int code { get; set; }
		public List<Product> result { get; set; }
	}
}

