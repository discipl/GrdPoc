namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_budget_tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetBalanceTransactions", "OwnerBudgetId", c => c.Int());
            AddColumn("dbo.Budgets", "BudgetInitialBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.BudgetBalanceTransactions", "OwnerBudgetId");
            AddForeignKey("dbo.BudgetBalanceTransactions", "OwnerBudgetId", "dbo.Budgets", "BudgetId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetBalanceTransactions", "OwnerBudgetId", "dbo.Budgets");
            DropIndex("dbo.BudgetBalanceTransactions", new[] { "OwnerBudgetId" });
            DropColumn("dbo.Budgets", "BudgetInitialBalance");
            DropColumn("dbo.BudgetBalanceTransactions", "OwnerBudgetId");
        }
    }
}
