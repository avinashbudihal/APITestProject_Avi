using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestProject_Avi.ServiceLayerUnitTests
{
    public class Service_UnitTests
    {
        /// <summary>
		/// Below test validates deposit method
		/// </summary>
		[Test]
        public async Task Deposit_Test()
        {
            var client = new HttpClient();
            var request = CreateHttpClientRequest(HttpMethod.Post, "deposit", 1000);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public HttpRequestMessage CreateHttpClientRequest(HttpMethod httpMethodType, string requestMethod, int amount = 0)
        {
            var request = new HttpRequestMessage(httpMethodType, $"{"http://localhost:8080/onlinewallet/"}{requestMethod}");
            request.Headers.Add("accept", "text/plain");
            if (httpMethodType == HttpMethod.Post)
                request.Content = CreateJsonContent(amount);

            return request;
        }

        public StringContent CreateJsonContent(int amount)
        {
            string jsonString = $"{{\"amount\": {amount}}}";
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
