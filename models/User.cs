using System;

namespace XmazonProject.Models
{
	public class User
	{
		public string uid { get; set; }
		public string email { get; set; }
		public int username { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }

		public User ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[User: uid={0}, email={1}, username={2}, firstname={3}, lastname={4}]", uid, email, username, firstname, lastname);
		}
	}
}

