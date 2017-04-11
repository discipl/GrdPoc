using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GrdPoc.Services
{
    public class SmartContractService : BaseService
    {
        public Contract contract { get; set; }

        //public string contractByteCode = "0x606060405260405160208060ae833981016040528080519060200190919050505b806000600050819055505b5060768060386000396000f360606040526000357c010000000000000000000000000000000000000000000000000000000090048063c6888fa1146037576035565b005b604b60048080359060200190919050506061565b6040518082815260200191505060405180910390f35b6000600060005054820290506071565b91905056";
        //public string abi = @"[{""constant"":false,""inputs"":[{""name"":""a"",""type"":""uint256""}],""name"":""multiply"",""outputs"":[{""name"":""d"",""type"":""uint256""}],""type"":""function""},{""inputs"":[{""name"":""multiplier"",""type"":""uint256""}],""type"":""constructor""}]";

        public string contractByteCode { get; set; }
        public string abi { get; set; }

        public string contractAddress { get; set; }

        public SmartContractService(string senderAddress, string senderPassword)
        {
            this.senderAddress = senderAddress;
            this.senderPassword = senderPassword;
        }

        public SmartContractService(string senderAddress, string senderPassword, Web3 web3)
        {
            this.senderAddress = senderAddress;
            this.senderPassword = senderPassword;
            this.web3 = web3;
        }


        public async Task<string> SendToServer(string contractByteCode, string abi)
        {
            //Store Variables for further interaction
            this.contractByteCode = contractByteCode;
            this.abi = abi;

            //unlock Senders account
            var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, senderPassword, new HexBigInteger(120));

            Assert.True(unlockAccountResult);

            //create a Transaction
            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, contractByteCode, senderAddress, new HexBigInteger(1000000));

            //start Miner
            var mineResult = await web3.Miner.Start.SendRequestAsync(6);

            Assert.True(mineResult);

            //wait for the Transaction Receipt 
            TransactionReceipt receipt = null;
            while (receipt == null)
            {
                await Task.Delay(500);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            //stop Miner
            mineResult = await web3.Miner.Stop.SendRequestAsync();
            Assert.True(mineResult);

            //Store Variables for further interaction
            contractAddress = receipt.ContractAddress;
            contract = web3.Eth.GetContract(abi, contractAddress);

            //return the Address where the contract was stored
            return receipt.ContractAddress;
        }


        public async Task<string> SendToServer(string contractByteCode, string abi, params object[] values)
        {
            //Store Variables for further interaction
            this.contractByteCode = contractByteCode;
            this.abi = abi;

            //unlock Senders account
            var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, senderPassword, new HexBigInteger(120));

            Assert.True(unlockAccountResult);

            //create a Transaction
            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, contractByteCode, senderAddress, new HexBigInteger(1000000), values);

            //start Miner
            var mineResult = await web3.Miner.Start.SendRequestAsync(6);

            Assert.True(mineResult);

            //wait for the Transaction Receipt 
            TransactionReceipt receipt = null;
            while (receipt == null)
            {
                await Task.Delay(500);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            //stop Miner
            mineResult = await web3.Miner.Stop.SendRequestAsync();
            Assert.True(mineResult);

            //Store Variables for further interaction
            contractAddress = receipt.ContractAddress;
            contract = web3.Eth.GetContract(abi, contractAddress);

            //return the Address where the contract was stored
            return receipt.ContractAddress;
        }



    }
}