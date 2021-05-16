using System;
using System.Net;
using Utils.Types.String;

namespace Utils.Net
{
    public class Web
    {
        public static HttpStatusCode GetUrlStatus(string url, string userAgent)
        {
            if (!url.Valid())
                throw new InvalidOperationException("Input url either null/empty or whitespace!");
            if (!userAgent.Valid())
                throw new InvalidOperationException("Input userAgent either null/empty or whitespace!");
            
            var result = default(HttpStatusCode);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = userAgent;
            request.Method = "HEAD";

            try
            {
                using var response = request.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    result = response.StatusCode;
                    response.Close();
                }
            }
            catch (WebException wEx)
            {
                if (wEx.Response != null)
                    result = ((HttpWebResponse) wEx.Response).StatusCode;
            }

            return result;
        }
    }
}