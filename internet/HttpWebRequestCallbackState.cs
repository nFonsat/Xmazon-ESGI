using System;
using System.IO;
using System.Net;


namespace XmazonProject.Internet
{
	public class HttpWebRequestCallbackState
	{
		public Stream ResponseStream { get; private set; }
		public WebException Exception { get; private set; }
		public Object State { get; set; }

		public HttpWebRequestCallbackState()
		{
		}

		public HttpWebRequestCallbackState(Stream responseStream, object state)
		{
			ResponseStream = responseStream;
			State = state;
		}

		public HttpWebRequestCallbackState(WebException exception)
		{
			Exception = exception;
		}
	}
}

