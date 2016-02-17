using System;
using System.Net;

namespace XmazonProject.Internet
{
	public class HttpWebRequestAsyncState
	{
		public byte[] RequestBytes { get; set; }
		public HttpWebRequest HttpWebRequest { get; set; }
		public Action<HttpWebRequestCallbackState> ResponseCallback { get; set; }
		public Object State { get; set; }

		public HttpWebRequestAsyncState()
		{
			
		}
	}
}

