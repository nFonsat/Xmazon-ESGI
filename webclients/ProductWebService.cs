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

		public void GetList (Action<HttpWebRequestCallbackState> responseCallback,
			string category_uid = null,
			string search = null,
			int limit = -1,
			int offset = -1
			)
		{
			NameValueCollection paramsCollection = new NameValueCollection ();
			if (category_uid != null) {
				paramsCollection.Set ("category_uid", category_uid);
			}

			if (search != null) {
				paramsCollection.Set ("search", search);
			}

			if (limit > 0) {
				paramsCollection.Set ("limit", Convert.ToString(limit));
			}

			if (offset > 0) {
				paramsCollection.Set ("offset", Convert.ToString(offset));
			}

			StringBuilder sb = new StringBuilder();

			foreach (string key in paramsCollection.AllKeys) {
				string value = paramsCollection [key];
				if (value != null) {
					sb.Append(string.Format("{0}={1}&", key, value));
				}
			}

			if (sb.Length > 0) {
				sb.Length -= 1;
			}

			string url = String.Format ("{0}{1}?{2}", BaseUrl, "/list", sb.ToString());

			OAuthHttpXamarin http = new OAuthHttpXamarin (url, "GET", 
				"application/x-www-form-urlencoded", OAuthContext.UserContext);

			http.ExecuteAsync (responseCallback);
		}
	}
}

