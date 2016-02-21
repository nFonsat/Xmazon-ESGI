using System;
using XmazonProject.Internet;

namespace XmazonProject.WebService
{
	public class UserWebService : BaseWebService
	{
		private const string BASE_USER = "/user";

		private static readonly UserWebService instance = new UserWebService();

		private UserWebService() : base(BASE_USER){}

		public static UserWebService Instance
		{
			get 
			{
				return instance; 
			}
		}



		public void GetUser (Action<HttpWebRequestCallbackState> responseCallback)
		{
			OAuthHttpXamarin http = new OAuthHttpXamarin (BaseUrl, 
				"GET", "application/x-www-form-urlencoded", OAuthContext.AppContext);

			http.ExecuteAsync (responseCallback);
		}
	}
}

