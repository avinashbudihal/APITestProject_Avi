using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using APITestProject_Avi.DTOs;
using APITestProject_Avi.Utility;

namespace APITestProject_Avi.E2E_Tests
{
    [TestFixture]
    public class E2E_Tests
    {
        #region Constructor

        /// <summary>
        /// Constructor of E2E_Tests class
        /// </summary>
        public E2E_Tests()
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

        #region E2E Scenarios

        /// <summary>
        /// This end-to-end test verifies the consecutive deposit and withdrawal actions
        /// </summary>
        [Test]
        [TestCase(600,5)]
        public void Verify_DepositAndWithdrawAmounts(double amountToDeposit, double amountToWithdraw)
        {
            try
            {
                // Get initial balance
                var balanceResponse = _httpMethod.GetMethod("balance");
                balanceResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                balanceResponse.Content.Should().NotBeNullOrEmpty();
                double initialBalance = JsonConvert.DeserializeObject<Amount>(balanceResponse.Content).amount;

                // Deposit amount
                var depositResponse = _httpMethod.PostMethod("deposit", new Amount { amount = amountToDeposit });
                depositResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                depositResponse.Content.Should().NotBeNullOrEmpty();
                double currentBalance = JsonConvert.DeserializeObject<Amount>(depositResponse.Content).amount;
                // Amount to deposit can be 0 also, hence greater than or equal check added
                currentBalance.Should().BeGreaterThanOrEqualTo(initialBalance);

                // Withdraw amount
                var withdrawResponse = _httpMethod.PostMethod("withdraw", new Amount { amount = amountToWithdraw });
                withdrawResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                withdrawResponse.Content.Should().NotBeNullOrEmpty();
                double newBalance = JsonConvert.DeserializeObject<Amount>(withdrawResponse.Content).amount;
                newBalance.Should().Be(currentBalance - amountToWithdraw);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This end-to-end test verifies the withdraw of amount greater than balance
        /// </summary>
        [Test]
        public void Verify_WithdrawOfAmountGreaterThanBalance()
        {
            try
            {
                // Get initial balance
                var balanceResponse = _httpMethod.GetMethod("balance");
                balanceResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                balanceResponse.Content.Should().NotBeNullOrEmpty();
                double initialBalance = JsonConvert.DeserializeObject<Amount>(balanceResponse.Content).amount;

                // Withdraw amount greater than balance
                double amountToWithdraw = initialBalance + 1;
                var withdrawResponse = _httpMethod.PostMethod("withdraw", new Amount { amount = amountToWithdraw });
                withdrawResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                withdrawResponse.Content.Should().NotBeNullOrEmpty();
                var errorMessage = JsonConvert.DeserializeObject<ResponseErrorMessage>(withdrawResponse.Content);
                errorMessage.title.Should().Contain("Invalid withdrawal amount. There are insufficient funds.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This end-to-end test verifies multiple times deposit and withdraw operations
        /// </summary>
        [Test]
        [TestCase(900, 3)]
        public void Verify_DepositAndWithdrawMultipleTimes(double amountToDeposit, double amountToWithdraw)
        {
            try
            {
                // First round of deposit and withdraw
                Verify_DepositAndWithdrawAmounts(amountToDeposit, amountToWithdraw);

                // Second round of deposit and withdraw
                Verify_DepositAndWithdrawAmounts(amountToDeposit, amountToWithdraw);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
