namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixes_to_invoice_tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItems", "ProductIncomeCode", c => c.String());
            AddColumn("dbo.InvoiceItems", "ProductExpenseCode", c => c.String());
            AddColumn("dbo.InvoiceItems", "BudgetCode", c => c.String());
            AlterColumn("dbo.Invoices", "InvoiceVatTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "InvoiceVatTotal", c => c.Int(nullable: false));
            DropColumn("dbo.InvoiceItems", "BudgetCode");
            DropColumn("dbo.InvoiceItems", "ProductExpenseCode");
            DropColumn("dbo.InvoiceItems", "ProductIncomeCode");
        }
    }
}
