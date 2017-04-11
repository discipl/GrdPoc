using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace GrdPoc.Services
{
    public class ExampleContractService : BaseService
    {
        private const int smartContractId = 1;
        private const int decimalPlaces = 2;

        private Contract contract;

        private string abi = @"[{""constant"":true,""inputs"":[],""name"":""companyInfo"",""outputs"":[{""name"":""companyId"",""type"":""int256""},{""name"":""companyName"",""type"":""string""},{""name"":""vatNumber"",""type"":""int256""},{""name"":""kvkNumber"",""type"":""string""},{""name"":""iban"",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_invoiceId"",""type"":""uint256""},{""name"":""_serviceId"",""type"":""uint256""},{""name"":""_serviceName"",""type"":""string""},{""name"":""_value"",""type"":""int256""}],""name"":""addPaymentDivision"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_invoiceId"",""type"":""uint256""},{""name"":""_customerName"",""type"":""string""},{""name"":""_customerVatNumber"",""type"":""int256""},{""name"":""_invoiceValue"",""type"":""int256""},{""name"":""_invoiceVatValue"",""type"":""int256""},{""name"":""_issueDate"",""type"":""uint256""},{""name"":""_dueDate"",""type"":""uint256""},{""name"":""_createdDate"",""type"":""uint256""}],""name"":""addInvoice"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[],""name"":""numberOfInvoices"",""outputs"":[{""name"":""retVal"",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimalPlaces"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""type"":""function""},{""inputs"":[{""name"":""_companyId"",""type"":""int256""},{""name"":""_companyName"",""type"":""string""},{""name"":""_vatNumber"",""type"":""int256""},{""name"":""_kvkNumber"",""type"":""string""},{""name"":""_iban"",""type"":""string""}],""payable"":false,""type"":""constructor""}]";

        private string contractAddress;

        public ExampleContractService(string senderAddress, string senderPassword)
        {
            this.senderAddress = senderAddress;
            this.senderPassword = senderPassword;
            InitContract();
        }

        public ExampleContractService(string senderAddress, string senderPassword, Web3 web3)
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


        //function InvoiceControl(int _companyId, string _companyName, int _vatNumber, string _kvkNumber,  string _iban)
        public static object[] ConstructorValues(int companyId, string companyName, int vatNumber, string kvkNumber, string iban)
        {
            var result = new object[] { companyId, companyName, vatNumber, kvkNumber, iban };

            return result;
        }

        //function addInvoice(uint _invoiceId, string _customerName, int _customerVatNumber, int _invoiceValue, int _invoiceVatValue, uint _issueDate, uint _dueDate, uint _createdDate)
        public async Task addInvoice(uint invoiceId, string customerName, int customerVatNumber, decimal invoiceValue, decimal invoiceVatValue, DateTime issueDate, DateTime dueDate, DateTime createdDate)
        {
            var unlockResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, passPhrase, new HexBigInteger(120));
            Assert.True(unlockResult);

            var addInvoiceFunction = contract.GetFunction("addInvoice");

            var transactionHash = await addInvoiceFunction.SendTransactionAsync(senderAddress, invoiceId, customerName, customerVatNumber, Decimal(invoiceValue, decimalPlaces), Decimal(invoiceVatValue, decimalPlaces), UnixTimeStamp(issueDate), UnixTimeStamp(dueDate), UnixTimeStamp(createdDate));

            var receipt = await MineAndGetReceiptAsync(transactionHash);
        }

        //function addPaymentDivision(uint _invoiceId, uint _serviceId, string _serviceName, int _value)
        public async Task addPaymentDivision(uint invoiceId, uint serviceId, string serviceName, decimal value)
        {
            var unlockResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, passPhrase, new HexBigInteger(120));
            Assert.True(unlockResult);

            var addPaymentDivisionFunction = contract.GetFunction("addPaymentDivision");

            var transactionHash = await addPaymentDivisionFunction.SendTransactionAsync(senderAddress, invoiceId, serviceId, serviceName, Decimal(value, decimalPlaces));

            var receipt = await MineAndGetReceiptAsync(transactionHash);
        }

        //function numberOfInvoices()
        public async Task<uint> numberOfInvoices()
        {
            var addPaymentDivisionFunction = contract.GetFunction("numberOfInvoices");

            var result = await addPaymentDivisionFunction.CallAsync<uint>();

            return result;
        }

    }

}