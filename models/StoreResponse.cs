using System;
using System.Collections.Generic;

namespace XmazonProject
{
	public class StoreResponse
	{
		public StoreResponse ()
		{
		}

		public int code { get; set; }
		public List<Store> result { get; set; }
	}
}

