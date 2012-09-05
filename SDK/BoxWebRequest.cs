using System;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2
{
    public class BoxWebRequest
    {
        /// <summary>
        /// Execute HTTP GET
        /// </summary>
        public static Stream ExecuteGET(string url, string authHeader)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add(HttpRequestHeader.Authorization, authHeader);            

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return response.GetResponseStream();            
        }

        /// <summary>
        /// Execute HTTP POST 
        /// </summary>
        public static Stream ExecutePOST(string url, string authHeader, byte[] requestData)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add(HttpRequestHeader.Authorization, authHeader);
            // request.ContentType = "application/xml";

            request.ContentLength = requestData.Length;
            
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestData, 0, requestData.Length);
            
            // Execute
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            
            return response.GetResponseStream();
        }

        /// <summary>
        /// Execute HTTP PUT
        /// </summary>
        public static Stream ExecutePUT(string url, string authHeader, byte[] requestData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "PUT";
            request.Headers.Add(HttpRequestHeader.Authorization, authHeader);
            request.ContentLength = requestData.Length;
            
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestData, 0, requestData.Length);

            // Execute
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                Console.WriteLine("WebException in HTTP PUT: " + webEx.Message);
                return null;
            }
           
            return response.GetResponseStream();
        }

        /// <summary>
        /// Execute HTTP DELETE 
        /// </summary>
        public static Stream ExecuteDELETE(string url, string authHeader)
        {
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = "DELETE";
            request.Headers.Add(HttpRequestHeader.Authorization, authHeader);

            // Execute
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                Console.WriteLine("WebException in HTTP DELETE: " + webEx.Message);
                return null;
            }

            return response.GetResponseStream();
        }
    }
}
