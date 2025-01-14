using APITestProject_Avi.DTOs;
using APITestProject_Avi.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITestProject_Avi.E2E_Tests
{
    public class E2E_Tests
    {
        #region E2E Scenarios

        /// <summary>
        /// This end-to-end test verifies the consecutive deposit and withdrawal actions
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task Verify_DepositAndWithdrawAmounts()
        {
            try
            {
                // get balance
                // deposite 600
                // get balance
                // withdraw 150
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This end-to-end test verifies the withdraw of amount greater than balance
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task Verify_WithdrawOfAmountGreaterThanBalance()
        {
            try
            {
                // get balance
                // Try to withdraw amount = balance+10
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This end-to-end test verifies the withdraw of amount greater than balance
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task Verify_TryToWithdrawAmountWhenBalanceIsZero()
        {
            try
            {
                // get balance
                // Withdraw amount = balance
                // get balance
                // Try to withdraw 60
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
