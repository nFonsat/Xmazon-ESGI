using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.IO;


namespace XmazonProject.Internet
{
	public partial class HttpXamarin
	{
		public string Url  { get; private set; }
		public string Method  { get; private set; }
		public string ContentType  { get; private set; }
		public NameValueCollection PostParameters  { get; private set; }
		protected HttpWebRequest _Request;


		protected HttpXamarin () 
		{
		}

		public HttpXamarin (string url, string httpMethod)
		{
			Url = url;
			Method = httpMethod;
			ContentType = "application/x-www-form-urlencoded";
		}

		public HttpXamarin (string url, string httpMethod, string contentType)
		{
			Url = url;
			Method = httpMethod;
			ContentType = contentType;
		}

		public HttpXamarin (string url, string httpMethod, string contentType, NameValueCollection postParameters)
		{
			Url = url;
			Method = httpMethod;
			ContentType = contentType;
			PostParameters = postParameters;
		}

		protected virtual HttpWebRequest CreateHttpWebRequest(string url, string httpMethod, string contentType)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

			httpWebRequest.ContentType = contentType;
			httpWebRequest.Method = httpMethod;

			return httpWebRequest;
		}

		protected byte[] GetRequestBytes(NameValueCollection postParameters)
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

