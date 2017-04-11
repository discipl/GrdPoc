using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GrdPoc.Models;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;

namespace GrdPoc.Services
{
    public class TestContractService : BaseService
    {
        private const int smartContractId = 4;
        //private ApplicationDbContext db = new ApplicationDbContext();

        private Contract contract;

        private string abi = @"[{""constant"":false,""inputs"":[{""name"":""val"",""type"":""int256""}],""name"":""multiply"",""outputs"":[{""name"":""d"",""type"":""int256""}],""type"":""function""},{""inputs"":[{""name"":""multiplier"",""type"":""int256""}],""type"":""constructor""}]";

        private string contractAddress;

        public TestContractService(string senderAddress, string senderPassword)
        {
            this.senderAddress = senderAddress;
            this.senderPassword = senderPassword;
            InitContract();
        }

        public TestContractService(string senderAddress, string senderPassword, Web3 web3)
        {
            this.senderAddress = senderAddress;
            this.senderPassword = senderPassword;
            this.web3 = web3;
            InitContract();
        }

        private void InitContract()
        {
            var smartContract = db.SmartContracts.Find(smartContractId);

            contractAddress = smartContract.SmartContractAddress;
            abi = smartContract.SmartContractAbi;

            contract = web3.Eth.GetContract(abi, contractAddress);
        }

        public void SetContract(Contract contract)
        {
            this.contract = contract;
        }


        public async Task<int> Multiply(int value)
        {
            var multiplyFunction = contract.GetFunction("multiply");

            var result = await multiplyFunction.CallAsync<int>(value);

            return result;
        }


        public async Task ShouldBeAbleCallAndReadEventLogs()
        {
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";

            var abi = @"[{'constant':false,'inputs':[{'name':'a','type':'int256'}],'name':'multiply','outputs':[{'name':'r','type':'int256'}],'type':'function'},{'inputs':[{'name':'multiplier','type':'int256'}],'type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'name':'a','type':'int256'},{'indexed':true,'name':'sender','type':'address'},{'indexed':false,'name':'result','type':'int256'}],'name':'Multiplied','type':'event'}]";

            var byteCode = "0x6060604052604051602080610104833981016040528080519060200190919050505b806000600050819055505b5060ca8061003a6000396000f360606040526000357c0100000000000000000000000000000000000000000000000000000000900480631df4f144146037576035565b005b604b60048080359060200190919050506061565b6040518082815260200191505060405180910390f35b60006000600050548202905080503373ffffffffffffffffffffffffffffffffffffffff16827f841774c8b4d8511a3974d7040b5bc3c603d304c926ad25d168dacd04e25c4bed836040518082815260200191505060405180910390a380905060c5565b91905056";

            var multiplier = 7;

            var web3 = new Web3();

            var unlockResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, passPhrase, new HexBigInteger(120));
            Assert.True(unlockResult);

            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, new HexBigInteger(900000), multiplier);
            var receipt = await MineAndGetReceiptAsync(transactionHash);

            var contractAddress = receipt.ContractAddress;

            var contract = web3.Eth.GetContract(abi, contractAddress);

            var multiplyFunction = contract.GetFunction("multiply");

            var multiplyEvent = contract.GetEvent("Multiplied");

            var filterAll = await multiplyEvent.CreateFilterAsync();
            var filter7 = await multiplyEvent.CreateFilterAsync(7);

            transactionHash = await multiplyFunction.SendTransactionAsync(senderAddress, 7);
            transactionHash = await multiplyFunction.SendTransactionAsync(senderAddress, 8);

            receipt = await MineAndGetReceiptAsync(transactionHash);


            var log = await multiplyEvent.GetFilterChanges<MultipliedEvent>(filterAll);
            var log7 = await multiplyEvent.GetFilterChanges<MultipliedEvent>(filter7);

        }

        //event Multiplied(int indexed a, address indexed sender, int result );

        public class MultipliedEvent
        {
            [Parameter("int", "a", 1, true)]
            public int MultiplicationInput { get; set; }

            [Parameter("address", "sender", 2, true)]
            public string Sender { get; set; }

            [Parameter("int", "result", 3, false)]
            public int Result { get; set; }

        }


    }
}