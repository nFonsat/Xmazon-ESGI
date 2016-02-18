﻿using System;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace XmazonProject.Internet
{
	public partial class HttpXamarin
	{
		public virtual HttpWebResponse ExecuteSync ()
		{
			HttpWebResponse webResponse = null;
			Stream requestStream = null;

			_Request = CreateHttpWebRequest(Url, Method, ContentType);

			try 
			{
				if ((Method.Equals ("POST") || Method.Equals ("PUT") || Method.Equals ("DELETE")) && PostParameters != null) {
					byte[] requestBytes = GetRequestBytes (PostParameters);
					_Request.ContentLength = requestBytes.Length;
					requestStream = _Request.GetRequestStream ();
					requestStream.Write (requestBytes, 0, requestBytes.Length);
					requestStream.Close ();
					requestStream = null;
				}

				webResponse = (HttpWebResponse)_Request.GetResponse ();
			}
			catch (WebException exception) 
			{
				webResponse = (HttpWebResponse)exception.Response;
			}
			finally
			{
				if (requestStream != null) {
					requestStream.Close ();
					requestStream = null;
				}
			}

			return webResponse;
		}
	}
}

