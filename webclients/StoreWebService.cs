using System;
using XmazonProject.Internet;
using System.Collections.Specialized;

namespace XmazonProject.WebService
{
	public class StoreWebService : BaseWebService
	{
		private const string BASE_STORE = "/store";

		private static readonly StoreWebService instance = new StoreWebService();

		private StoreWebService() : base(BASE_STORE){}

		public static StoreWebService Instance
		{
			get 
			{
				return instance; 
			}
		}
			
		public void CreateStore (string name,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection cartCollection = new NameValueCollection ();
			cartCollection.Set ("name", name);

			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format("{0}{1}", BaseUrl,  "/add"), 
				"PUT", "application/x-www-form-urlencoded", 
				cartCollection, OAuthContext.AppContext);

			http.ExecuteAsync (responseCallback);
		}

		public void GetList (Action<HttpWebRequestCallbackState> responseCallback)
		{
			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format ("{0}{1}", BaseUrl, "/list"), 
				"GET", "application/x-www-form-urlencoded", OAuthContext.AppContext);

			http.ExecuteAsync (responseCallback);
		}
	}
}

