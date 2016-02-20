using System;

namespace XmazonProject.Models
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

		public override string ToString ()
		{
			return string.Format ("[AccessToken: token_type={0}, access_token={1}, expires_in={2}, refresh_token={3}]", token_type, access_token, expires_in, refresh_token);
		}
	}
}

