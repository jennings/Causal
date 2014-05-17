using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace Causal.Updater.v1
{
    internal static class HttpResponseFactory
    {
        public static HttpResponseException NotFound()
        {
            return new HttpResponseException(HttpStatusCode.NotFound);
        }

        public static HttpResponseException BadRequest(string message)
        {
            return Generic(HttpStatusCode.BadRequest, message);
        }

        public static HttpResponseException Generic(HttpStatusCode statusCode, string message)
        {
            var responseMessage = new HttpResponseMessage(statusCode);
            var responseContent = new { Error = message };
            var jsonResponseContent = JsonConvert.SerializeObject(responseContent);
            responseMessage.Content = new StringContent(jsonResponseContent);
            return new HttpResponseException(responseMessage);
        }
    }
}
