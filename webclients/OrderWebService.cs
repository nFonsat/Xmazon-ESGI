using System;
using XmazonProject.Internet;
using System.Collections.Specialized;
using System.Text;

namespace XmazonProject.WebService
{
	public class OrderWebService : BaseWebService
	{
		private const string BASE_ORDER = "/order";

		private static readonly OrderWebService instance = new OrderWebService();

		private OrderWebService() : base(BASE_ORDER){}

		public static OrderWebService Instance
		{
			get 
			{
				return instance; 
			}
		}

		public void CreateOrder (Action<HttpWebRequestCallbackState> responseCallback)
		{
			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format("{0}{1}", BaseUrl,  "/create"), 
				"PUT", "application/x-www-form-urlencoded", OAuthContext.UserContext);

			http.ExecuteAsync (responseCallback);
		}

		public void GetList (int limit, int offset,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection paramsCollection = new NameValueCollection ();
			paramsCollection.Set ("limit", limit);
			paramsCollection.Set ("offset", offset);

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

