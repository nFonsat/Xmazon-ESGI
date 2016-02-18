using System;
using XmazonProject.Internet;
using System.Collections.Specialized;
using System.Text;

namespace XmazonProject.WebService
{
	public class ProductWebService : BaseWebService
	{
		private const string BASE_PRODUCT = "/product";

		private static readonly ProductWebService instance = new ProductWebService();

		private ProductWebService() : base(BASE_PRODUCT){}

		public static ProductWebService Instance
		{
			get 
			{
				return instance; 
			}
		}

		public void CreateProduct (string name,
			float price,
			string category_uid,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection cartCollection = new NameValueCollection ();
			cartCollection.Set ("name", name);
			cartCollection.Set ("price", Convert.ToString(price));
			cartCollection.Set ("category_uid", category_uid);

			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format("{0}{1}", BaseUrl,  "/add"), 
				"PUT", "application/x-www-form-urlencoded", 
				cartCollection, OAuthContext.AppContext);

			http.ExecuteAsync (responseCallback);
		}

		public void GetList (string category_uid,
			string search,
			int limit,
			int offset,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection paramsCollection = new NameValueCollection ();
			paramsCollection.Set ("category_uid", category_uid);
			paramsCollection.Set ("search", search);
			paramsCollection.Set ("limit", Convert.ToString(limit));
			paramsCollection.Set ("offset", Convert.ToString(offset));

			StringBuilder sb = new StringBuilder();

			foreach (string key in paramsCollection.AllKeys) {
				string value = paramsCollection [key];
				if (value != null) {
					sb.Append(string.Format("{0}={1}&", key, value));
				}
			}

			sb.Length -= 1; 
			string url = String.Format ("{0}{1}?{2}", BaseUrl, "/list", sb.ToString());

			OAuthHttpXamarin http = new OAuthHttpXamarin (url, "GET", 
				"application/x-www-form-urlencoded", OAuthContext.UserContext);

			http.ExecuteAsync (responseCallback);
		}
	}
}

