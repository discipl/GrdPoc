using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class BlockchainAccount
    {
        [Key]
        public int BlockChainAccountId { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Account Blockchain Address")]
        public string AccountAddress { get; set; }

        [Display(Name = "Account Password")]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [Display(Name = "Active")]
        public bool AccountActive { get; set; }

        [Display(Name = "Prefered Blockchain Server")]
        [ForeignKey("PreferedBlockChainNodeServer")]
        public int? PreferedBlockChainNodeServerId { get; set; }
        public virtual BlockchainNodeServer PreferedBlockChainNodeServer { get; set; }

    }
}