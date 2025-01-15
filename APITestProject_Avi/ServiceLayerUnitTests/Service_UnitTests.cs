using System.Net;
using System.Text;
using Newtonsoft.Json;
using FluentAssertions;
using APITestProject_Avi.DTOs;
using APITestProject_Avi.Utility;
using NUnit.Framework.Internal;

namespace APITestProject_Avi.ServiceLayerUnitTests
{
    public class Service_UnitTests
    {


        #region Happy Scenarios

        /// <summary>
        /// This test verify the deposit function
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task DepositTest()
        {
            try
            {
                const double amountValue = 1000;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{HttpCommonMethods.BaseUrl}deposit", content);

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

        /// <summary>
        /// This test verify the get balance function
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task GetBalanceTest()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{HttpCommonMethods.BaseUrl}{"balance"}");
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

        /// <summary>
        /// This test verify the withdraw function
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task WithdrawTest()
        {
            try
            {
                const double amountValue = 150;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{HttpCommonMethods.BaseUrl}withdraw", content);

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

        #region Negative Scenarios

        /// <summary>
        /// This test verify the deposit function
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task DepositNegativeAmountTest()
        {
            try
            {
                const double amountValue = -60;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{HttpCommonMethods.BaseUrl}deposit", content);

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                ResponseErrorMessage result = JsonConvert.DeserializeObject<ResponseErrorMessage>(await response.Content.ReadAsStringAsync());
                result.Should().NotBeNull();
                result.title.Should().Contain("validation errors occurred.");
                result.errors.Amount.First().Should().Contain("must be greater than or equal to '0'.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This test verify the withdraw function
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task WithdrawNegativeAmountTest()
        {
            try
            {
                const double amountValue = -600;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{HttpCommonMethods.BaseUrl}withdraw", content);

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                ResponseErrorMessage result = JsonConvert.DeserializeObject<ResponseErrorMessage>(await response.Content.ReadAsStringAsync());
                result.Should().NotBeNull();
                result.title.Should().Contain("validation errors occurred.");
                result.errors.Amount.First().Should().Contain("must be greater than or equal to '0'.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This test try to access invalid service
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task TryToAccessInvalidService()
        {
            try
            {
                const double amountValue = 351;
                var content = new StringContent(JsonConvert.SerializeObject(
                    new Amount { amount = amountValue }), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync($"{HttpCommonMethods.BaseUrl}withdraws", content);

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
