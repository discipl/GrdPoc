using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrdPoc
{
    public enum InvoiceStatus { Dispatched, Paid, Rejected }

    public enum ContractStatus { Initiated, ApprovalPending, Approved, Delivered, Confirmed, Faulty }

    public enum ProjectStatus { Unconfigured, Configured, Executing, Delivered, Confirmed, Faulty }
    public enum VatValues { Vat_0 = 0, Vat_21 = 21 }
}