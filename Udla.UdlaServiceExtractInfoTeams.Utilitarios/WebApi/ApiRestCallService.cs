using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Udla.UdlaServiceExtractInfoTeams.Util.WebApi
{
    /// <summary>
    /// Clase generica para realizar las llamadas a los servicios REST
    /// </summary>
    public class ApiRestCallService
    {

        #region log declaration
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// Función para ejecutar la llamada al servicio REST
        /// </summary>
        /// <param name="url">URL a la que se va a apuntar</param>
        /// <param name="jsonData">JSON Data</param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Execute(string url, string jsonData, string userName, string password)
        {
            string response = "";

            HttpWebRequest request = GetRequest(url, userName, password);
            //request.Timeout = 120000;

            AddBody(request, jsonData);

            response = GetResponse(request);

            return response;
        }

        private static HttpWebRequest GetRequest(string url, string userName, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            Uri myUri = new Uri(url);
            request.Method = "POST";
            //request.ContentType = "text/plain; charset=ISO-8859-1"; //"text/html; charset=ISO-8859-1"; //"application/x-www-form-urlencoded"; //"text/html; charset=utf-8"; //"iso-8859-1";
            //request.TransferEncoding

            NetworkCredential myNetworkCredential = new NetworkCredential(userName, password);
            CredentialCache myCredentialCache = new CredentialCache();
            myCredentialCache.Add(myUri, "Basic", myNetworkCredential);

            request.PreAuthenticate = true;
            request.Credentials = myCredentialCache;

            return request;
        }

        private static void AddBody(HttpWebRequest request, string jsonData)
        {
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
            }
        }

        private static string GetResponse(HttpWebRequest request)
        {
            string response = string.Empty;
            Stream responseStream = null;
            WebResponse myWebResponse = null;
            try
            {
                myWebResponse = request.GetResponse();

                responseStream = myWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                string pageContent = myStreamReader.ReadToEnd();
                string[] elementos = pageContent.Split('{');

                JObject rss = JObject.Parse(pageContent);
                response = rss[elementos[1].Replace('"', ' ').Replace(':', ' ').Trim()].ToString();
            }
            catch (Exception ex)
            {
                Log.Error($"Error en SapApiRestCallService:  {ex.Message}");
                throw;
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Flush();
                    responseStream.Close();
                    responseStream.Dispose();
                }
                if (myWebResponse != null)
                {
                    myWebResponse.Close();
                }
            }

            return response;
        }
    }
}
