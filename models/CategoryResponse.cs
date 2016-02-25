using System;
using System.Collections.Generic;

namespace XmazonProject
{
	public class CategoryResponse
	{
		public CategoryResponse ()
		{
		}

		public int code { get; set; }
		public List<Category> result { get; set; }
	}
}

