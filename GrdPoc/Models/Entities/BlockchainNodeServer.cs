using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class BlockchainNodeServer
    {
        [Key]
        public int BlockChainNodeServerId { get; set; }

        [Display(Name = "Node Server Name")]
        public string NodeServerName { get; set; }

        [Display(Name = "Name Address")]
        public string NodeServerNameAddress { get; set; }

        [Display(Name = "Ip Address")]
        public string NodeServerIpAddress { get; set; }

        [Display(Name = "Active")]
        public bool NodeServerActive { get; set; }
    }
}