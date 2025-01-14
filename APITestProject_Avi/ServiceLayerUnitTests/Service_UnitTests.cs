﻿using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using NUnit.Framework.Internal;
using APITestProject_Avi.DTOs;
using APITestProject_Avi.Utility;

namespace APITestProject_Avi.ServiceLayerUnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
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
        [Test, Order(1)]
        [TestCase(1000)]
        [TestCase(5.29)]
        public void DepositTest(double amountToDeposit)
        {
            try
            {
                // Deposit amount
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
        [Test, Order(2)]
        [TestCase(25)]
        [TestCase(25.5)]
        public async Task WithdrawTest(double amountToWithdraw)
        {
            try
            {
                // Withdraw amount (the value should be less than balance amount, this check achieved by specifying order to test method)
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
        [TestCase(-60)]
        public void DepositNegativeAmountTest(double amountToDeposit)
        {
            try
            {
                // Deposit negative amount
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
        [TestCase(-5)]
        public void WithdrawNegativeAmountTest(double amountToWithdraw)
        {
            try
            {
                // Withdraw negative amount
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
    }
}
