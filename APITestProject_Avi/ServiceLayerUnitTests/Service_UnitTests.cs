using System.Net;
using System.Text;
using Newtonsoft.Json;
using FluentAssertions;
using APITestProject_Avi.DTOs;
using APITestProject_Avi.Utility;

namespace APITestProject_Avi.ServiceLayerUnitTests
{
    public class Service_UnitTests
    {
        #region Happy scenarios

        [Test]
        public async Task DepositTest()
        {
            try
            {
                const double amountValue = 1000;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{Common.BaseUrl}deposit", content);

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                Amount result = JsonConvert.DeserializeObject<Amount>(await response.Content.ReadAsStringAsync());
                result.Should().NotBeNull();
                result.amount.Should().BeGreaterThanOrEqualTo(amountValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [Test]
        public async Task GetBalanceTest()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{Common.BaseUrl}{"balance"}");
                request.Headers.Add("accept", "text/plain");
                var response = await client.SendAsync(request);

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                Amount result = JsonConvert.DeserializeObject<Amount>(await response.Content.ReadAsStringAsync());
                result.Should().NotBeNull();
                result.amount.Should().BeGreaterThanOrEqualTo(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Test]
        public async Task WithdrawTest()
        {
            try
            {
                const double amountValue = 150;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{Common.BaseUrl}withdraw", content);

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                Amount result = JsonConvert.DeserializeObject<Amount>(await response.Content.ReadAsStringAsync());
                result.Should().NotBeNull();
                result.amount.Should().BeGreaterThanOrEqualTo(amountValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
