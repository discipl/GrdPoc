namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_invoice_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        Description = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        VatPercent = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDescription = c.String(),
                        InvoiceIssuerName = c.String(),
                        InvoiceIssuerAddress = c.String(),
                        InvoiceIssuerKvk = c.String(),
                        InvoiceIssuerBtw = c.String(),
                        InvoiceIssuerIban = c.String(),
                        InvoiceDate = c.DateTime(),
                        InvoiceDueDate = c.DateTime(),
                        InvoiceCustomerName = c.String(),
                        InvoiceCustomeAddress = c.String(),
                        InvoiceCustomeKvk = c.String(),
                        InvoiceCustomeBtw = c.String(),
                        InvoiceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceVatTotal = c.Int(nullable: false),
                        InvoiceValueTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceItems");
        }
    }
}
