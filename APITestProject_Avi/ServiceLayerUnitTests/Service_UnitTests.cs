using System.Net;
using System.Text;
using Newtonsoft.Json;
using FluentAssertions;
using NUnit.Framework.Internal;
using APITestProject_Avi.DTOs;
using APITestProject_Avi.Utility;
using System.Net.Http;

namespace APITestProject_Avi.ServiceLayerUnitTests
{
    public class Service_UnitTests
    {
        #region Constructor

        /// <summary>
        /// Constructor of Service_UnitTests class
        /// </summary>
        public Service_UnitTests()
        {
            _httpMethod = new HttpCommonMethods();
        }

        #endregion

        #region Properties

        /// <summary>
        /// property for HttpCommonMethods
        /// </summary>
        public HttpCommonMethods _httpMethod;

        #endregion

        #region Happy Scenarios

        /// <summary>
        /// This test verify the deposit function
        /// </summary>
        [Test]
        public void DepositTest()
        {
            try
            {
                // Deposit amount
                double amountToDeposit = 1000;
                var depositResponse = _httpMethod.PostMethod("deposit", new Amount { amount = amountToDeposit });
                depositResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                depositResponse.Content.Should().NotBeNullOrEmpty();
                double currentBalance = JsonConvert.DeserializeObject<Amount>(depositResponse.Content).amount;
                // Amount to deposit can be 0 also, hence greater than or equal check added
                currentBalance.Should().BeGreaterThanOrEqualTo(amountToDeposit);
            }
            catch (Exception ex)
            {
                throw ex;
            }      
        }

        /// <summary>
        /// This test verify the get balance function
        /// </summary>
        [Test]
        public void GetBalanceTest()
        {
            try
            {
                // Get balance
                var balanceResponse = _httpMethod.GetMethod("balance");
                balanceResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                balanceResponse.Content.Should().NotBeNullOrEmpty();
                double balance = JsonConvert.DeserializeObject<Amount>(balanceResponse.Content).amount;
                balance.Should().BeGreaterThanOrEqualTo(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This test verify the withdraw function
        /// </summary>
        [Test]
        public async Task WithdrawTest()
        {
            try
            {
                // Withdraw amount (0 if no balance)
                double amountToWithdraw = 0;
                var withdrawResponse = _httpMethod.PostMethod("withdraw", new Amount { amount = amountToWithdraw });
                withdrawResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                withdrawResponse.Content.Should().NotBeNullOrEmpty();
                double newBalance = JsonConvert.DeserializeObject<Amount>(withdrawResponse.Content).amount;
                newBalance.Should().BeGreaterThanOrEqualTo(amountToWithdraw);
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
        [Test]
        public void DepositNegativeAmountTest()
        {
            try
            {
                // Deposit negative amount
                double amountToDeposit = -60;
                var depositResponse = _httpMethod.PostMethod("deposit", new Amount { amount = amountToDeposit });
                depositResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                depositResponse.Content.Should().NotBeNullOrEmpty();
                ResponseErrorMessage errorMessage = JsonConvert.DeserializeObject<ResponseErrorMessage>(depositResponse.Content);
                // Amount to deposit can be 0 also, hence greater than or equal check added
                errorMessage.title.Should().Contain("validation errors occurred.");
                errorMessage.errors.Amount.First().Should().Contain("must be greater than or equal to '0'.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This test verify the withdraw function
        /// </summary>
        [Test]
        public void WithdrawNegativeAmountTest()
        {
            try
            {
                // Withdraw negative amount
                double amountToWithdraw = -40;
                var withdrawResponse = _httpMethod.PostMethod("withdraw", new Amount { amount = amountToWithdraw });
                withdrawResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                withdrawResponse.Content.Should().NotBeNullOrEmpty();
                ResponseErrorMessage errorMessage = JsonConvert.DeserializeObject<ResponseErrorMessage>(withdrawResponse.Content);
                errorMessage.title.Should().Contain("validation errors occurred.");
                errorMessage.errors.Amount.First().Should().Contain("must be greater than or equal to '0'.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This test try to access invalid service
        /// </summary>
        [Test]
        public void TryToAccessInvalidService()
        {
            try
            {
                // Define an invalid service endpoint
                string invalidEndpoint = "invalid";
                double amountToWithdraw = 0;
                var withdrawResponse = _httpMethod.PostMethod(invalidEndpoint, new Amount { amount = amountToWithdraw });
                withdrawResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Edge case

        /// <summary>
        /// This test verify the deposit of big fractional value
        /// </summary>
        [Test]
        public void DepositFractionalValueTest()
        {
            try
            {
                // Deposit amount
                double amountToDeposit = 1.256776786785673333764746547564;
                var depositResponse = _httpMethod.PostMethod("deposit", new Amount { amount = amountToDeposit });
                depositResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                depositResponse.Content.Should().NotBeNullOrEmpty();
                double currentBalance = JsonConvert.DeserializeObject<Amount>(depositResponse.Content).amount;
                // Amount to deposit can be 0 also, hence greater than or equal check added
                currentBalance.Should().BeGreaterThanOrEqualTo(amountToDeposit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
