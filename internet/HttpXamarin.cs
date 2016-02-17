using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.IO;


namespace XmazonProject.Internet
{
	public partial class HttpXamarin
	{
		private HttpXamarin ()
		{
		}

		protected static HttpWebRequest CreateHttpWebRequest(string url, string httpMethod, string contentType)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

			httpWebRequest.ContentType = contentType;
			httpWebRequest.Method = httpMethod;

			return httpWebRequest;
		}

		protected static byte[] GetRequestBytes(NameValueCollection postParameters)
		{
			if (postParameters == null || postParameters.Count == 0) {
				return new byte[0];
			}

			StringBuilder sb = new StringBuilder();

			foreach (string key in postParameters.AllKeys) {
				sb.Append(string.Format("{0}={1}&", key, postParameters[key]));
			}

			sb.Length -= 1; 
			return Encoding.UTF8.GetBytes(sb.ToString());
		}

		public static string GetResponseText(Stream responseStream)
		{
			using (StreamReader reader = new StreamReader(responseStream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}

