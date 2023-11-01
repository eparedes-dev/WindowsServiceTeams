using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Udla.UdlaServiceExtractInfoTeams.Util.WebApi
{
    public class WebApiInvoker
    {
        public string BaseAddress { get; set; }

        public WebApiInvoker(string baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        /// <summary>
        /// Do a Post request
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string Post(string requestUri, object parameter, string securityToken)
        {
            var jsonRequest = JsonConvert.SerializeObject(parameter);
            return this.Post(requestUri, jsonRequest, securityToken);
        }

        public async Task<string> PostJson(string requestUri, string json, string securityToken)
        {
            string result = string.Empty;
            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", securityToken);
            HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        //public async Task<string> PostJson(string requestUri, string json, string securityToken)
        //{
        //    string result = string.Empty;
        //    HttpClient client = new HttpClient();
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", securityToken);
        //    HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = response.Headers.Location.ToString();
        //    }
        //    return result;
        //}


        /// <summary>
        /// Do a Post request
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string Post(string requestUri, string jsonData, string securityToken)
        {
            string response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", securityToken);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = client.PostAsync(requestUri, content).GetAwaiter().GetResult();

                response = httpResponse.Content.ReadAsStringAsync().Result;
            }

            return response;
        }

        /// <summary>
        /// Obtiene el Token de OAuth2.0
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string SecurityToken(string requestUri, List<KeyValuePair<string, string>> parameter)
        {
            string response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);

                var req = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new FormUrlEncodedContent(parameter)
                };

                HttpResponseMessage httpResponse = client.SendAsync(req).GetAwaiter().GetResult();

                response = httpResponse.Content.ReadAsStringAsync().Result;
            }

            return response;
        }

        /// <summary>
        /// Do a Get request
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string Get(string requestUri, string securityToken)
        {
            string response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", securityToken);

                HttpResponseMessage httpResponse = client.GetAsync(requestUri).GetAwaiter().GetResult();

                response = httpResponse.Content.ReadAsStringAsync().Result;
            }

            return response;
        }
    }
}
