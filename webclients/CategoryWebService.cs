using System;
using XmazonProject.Internet;
using System.Collections.Specialized;
using System.Text;

namespace XmazonProject.WebService
{
	public class CategoryWebService : BaseWebService
	{
		private const string BASE_CATEGORY = "/category";

		private static readonly CategoryWebService instance = new CategoryWebService();

		private CategoryWebService() : base(BASE_CATEGORY){}

		public static CategoryWebService Instance
		{
			get 
			{
				return instance; 
			}
		}
			
		public void GetList (string store_uid,
			string search,
			int limit,
			int offset,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection paramsCollection = new NameValueCollection ();
			paramsCollection.Set ("store_uid", store_uid);
			paramsCollection.Set ("search", search);
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
				"application/x-www-form-urlencoded", OAuthContext.AppContext);

			http.ExecuteAsync (responseCallback);
		}
	}
}

