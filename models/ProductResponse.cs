using System;
using System.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XmazonProject
{
	public class ProductResponse
	{
		public ProductResponse ()
		{
		}

		public ProductResponse (int code, List<Product> result)
		{
			this.code = code;
			this.result = result;
		}

		public int code { get; set; }
		public List<Product> result { get; set; }
	
		public static ProductResponse Deserialize (JsonValue jsonCategory)
		{
			String json = jsonCategory.ToString ();
//			JsonArray result = jsonCategory ["result"];

			return JsonConvert.DeserializeObject<ProductResponse>(json);
		}
	}
}

