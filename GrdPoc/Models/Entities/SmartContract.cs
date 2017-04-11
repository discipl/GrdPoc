using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class SmartContract
    {
        [Key]
        public int SmartContractId { get; set; }

        [Display(Name = "Smart Contract Name")]
        public string SmartContractName { get; set; }

        [Display(Name = "Bytecode")]
        public string SmartContractByteCode { get; set; }

        [Display(Name = "Abi")]
        public string SmartContractAbi { get; set; }

        [Display(Name = "Default Blockchain Address")]
        public string SmartContractAddress { get; set; }

        [Display(Name = "Smart Contract Source Code")]
        public string SmartContractCode { get; set; }

        [Display(Name = "Active")]
        public bool SmartContractActive { get; set; }

        [ForeignKey("BlockChainDeployAccount")]
        public int? BlockChainAccountId { get; set; }
        public virtual BlockchainAccount BlockChainDeployAccount { get; set; }

    }
}