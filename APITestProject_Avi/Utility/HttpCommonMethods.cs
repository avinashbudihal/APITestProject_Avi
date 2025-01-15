using RestSharp;
using System.Net;
using FluentAssertions;

namespace APITestProject_Avi.Utility
{
    public class HttpCommonMethods
    {
        /// <summary>
        /// base url of the service
        /// </summary>
        private const string _baseUrl = "http://localhost:8080/onlinewallet/";
        /// <summary>
        /// Peroperty which provides base url
        /// </summary>
        public static string BaseUrl
        {
            get { return _baseUrl; }
        }

        /// <summary>
        /// Get functionality
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>RestResponse</returns>
        public RestResponse GetMethod(string resourceName)
        {
            try
            {
                var client = new RestClient(_baseUrl);
                var response = client.ExecuteGet(new RestRequest(resourceName));
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post functionality
        /// </summary>
        /// <param name="resourceName">resourceName</param>
        /// <param name="obj">body object</param>
        /// <returns></returns>
        public RestResponse PostMethod(string resourceName, Object obj)
        {
            try
            {
                var client = new RestClient(_baseUrl);
                var request = new RestRequest(resourceName);
                request.AddBody(obj);
                var response = client.ExecutePost(request);
                response.Should().NotBeNull();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
