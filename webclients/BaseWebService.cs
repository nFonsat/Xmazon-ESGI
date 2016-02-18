using System;

namespace XmazonProject.WebService
{
	public class BaseWebService
	{
		protected const string BASE_API = "http://xmazon.appspaces.fr";
		
		protected string BaseUrl;
		
		public BaseWebService(string url)
		{
			BaseUrl = String.Format("{0}{1}", BASE_API,  url);
		}
	}
}

