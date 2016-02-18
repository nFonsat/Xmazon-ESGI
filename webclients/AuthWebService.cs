using System;
using XmazonProject.Internet;
using System.Collections.Specialized;

namespace XmazonProject.WebService
{
	public class AuthWebService : BaseWebService
	{
		private const string BASE_AUTH = "/auth";
		
		private static readonly AuthWebService instance = new AuthWebService();
		
		private AuthWebService() : base(BASE_AUTH){}

		public static AuthWebService Instance
		{
			get 
			{
				return instance; 
			}
		}

		public void SubscribeUser (string email,
			string password,
			string firstname,
			string lastname,
			string birthdate,
			Action<HttpWebRequestCallbackState> responseCallback)
		{
			NameValueCollection userCollection = new NameValueCollection ();
			userCollection.Set ("email", email);
			userCollection.Set ("password", password);
			userCollection.Set ("firstname", firstname);
			userCollection.Set ("lastname", lastname);
			userCollection.Set ("birthdate", birthdate);
			OAuthHttpXamarin http = new OAuthHttpXamarin (String.Format("{0}{1}", BaseUrl,  "/subscribe"), 
				"POST", 
				"application/x-www-form-urlencoded", 
				userCollection,
				OAuthContext.AppContext);
			http.ExecuteAsync (responseCallback);
		}
	}
}

