using System;
using System.IO;
using System.Net;
using Utils.Types.String;

namespace Utils.Net
{
    public class Web
    {
        public static HttpStatusCode GetUrlStatus(string url, string userAgent)
        {
            if (!url.Valid())
                throw new InvalidOperationException("Input url is invalid!");
            if (!userAgent.Valid())
                throw new InvalidOperationException("Input userAgent is invalid!");
            
            var result = default(HttpStatusCode);
            var request = (HttpWebRequest) WebRequest.Create(url);
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

        public static string DownloadWebsiteAsHtml(string url, string userAgent, string outFilePath)
        {
            if (!url.Valid())
                throw new InvalidOperationException("Input url is invalid!");
            if (!userAgent.Valid())
                throw new InvalidOperationException("Input userAgent is invalid!");
            if (!outFilePath.Valid())
                throw new InvalidOperationException("Input outFilePath is invalid!");

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            if (request.Proxy != null)
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.UserAgent = userAgent;
            request.Accept = "*/*";

            var response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException("Error, response.StatusCode not OK: " + response.StatusCode);

            var responseStream = response.GetResponseStream();
            if (responseStream == null)
                throw new InvalidOperationException("Error, failed to get responseStream!");

            var reader = new StreamReader(responseStream);
            string[] result = { reader.ReadToEnd() };
            var docPath = outFilePath + url.ReplaceForbiddenFilenameChars("_") + ".html";
            File.WriteAllLines(docPath, result);

            return docPath;
        }
    }
}