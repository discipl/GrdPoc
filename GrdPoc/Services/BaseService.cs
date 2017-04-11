using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using GrdPoc.Models;
using Nethereum.RPC.Eth.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace GrdPoc.Services
{
    public class BaseService
    {
        private string defaultServerAddress = ConfigurationManager.AppSettings["EthereumServerAddress"].ToString();

        protected string passPhrase = "ThE~Bl0ckch@in-P@ssW0rd";

        protected ApplicationDbContext db = new ApplicationDbContext();

        protected string serverAddress;
        protected string senderAddress;
        protected string senderPassword;

        protected Web3 web3;

        public BaseService()
        {
            try
            {
                web3 = new Web3(defaultServerAddress);

                //serverAddress = db.BlockchainNodeServers.Where(w => w.NodeServerActive == true)
                //                                              .FirstOrDefault()?.NodeServerIpAddress ?? defaultServerAddress;
                //web3 = new Web3(serverAddress);
            }
            catch
            {
            }
        }

        protected static class Assert
        {
            public static void True(bool test)
            {
                if (!test)
                {
                    throw new Exception("Result was False!");
                }
            }

        }

        protected async Task<TransactionReceipt> MineAndGetReceiptAsync(string transactionHash)
        {

            var miningResult = await web3.Miner.Start.SendRequestAsync(6);
            Assert.True(miningResult);

            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            miningResult = await web3.Miner.Stop.SendRequestAsync();
            Assert.True(miningResult);
            return receipt;
        }

        protected Int32 UnixTimeStamp(DateTime datetime)
        {
            return (Int32)(datetime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        protected Int32 Decimal(decimal value, int decimalPlaces)
        {
            return Convert.ToInt32(Math.Round((value * (10 ^ decimalPlaces))));
        }


    }
}