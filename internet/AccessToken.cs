using System;
namespace XmazonProject.Internet
{
	public class AccessToken
	{
		public string token_type { get; set; }
		public string access_token { get; set; }
		public int expires_in { get; set; }
		public string refresh_token { get; set; }
		
		public AccessToken()
		{
		}
	}
}

