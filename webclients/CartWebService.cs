using System;
using XmazonProject.Internet;
using System.Collections.Specialized;

namespace XmazonProject.WebService
{
	public class CartWebService : BaseWebService
	{
		private const string BASE_CART = "/cart";

		private static readonly CartWebService instance = new CartWebService();

		private CartWebService() : base(BASE_CART){}

		public static CartWebService Instance
		{
			get 
			{
				return instance; 
			}
		}

		public void AddProduct (string productUid,
			int quantity,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection cartCollection = new NameValueCollection ();
			cartCollection.Set ("product_uid", productUid);
			cartCollection.Set ("quantity", quantity);

			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format("{0}{1}", BaseUrl,  "/add"), 
				"PUT", "application/x-www-form-urlencoded", 
				cartCollection, OAuthContext.UserContext);

			http.ExecuteAsync (responseCallback);
		}

		public void RemoveProduct (string productUid,
			int quantity,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection cartCollection = new NameValueCollection ();
			cartCollection.Set ("product_uid", productUid);
			cartCollection.Set ("quantity", quantity);

			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format("{0}{1}", BaseUrl,  "/remove"), 
				"DELETE", "application/x-www-form-urlencoded", 
				cartCollection, OAuthContext.UserContext);

			http.ExecuteAsync (responseCallback);
		}

		public void GetCart (Action<HttpWebRequestCallbackState> responseCallback)
		{
			OAuthHttpXamarin http = new OAuthHttpXamarin (BaseUrl, "GET", 
				"application/x-www-form-urlencoded", OAuthContext.UserContext);

			http.ExecuteAsync (responseCallback);
		}
	}
}

