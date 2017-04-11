namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_contracttype_foreignkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidentalContracts", "IncidentalContracTypeId", c => c.Int());
            AddColumn("dbo.ProductIncidentalContracts", "IncidentalContracTypeId", c => c.Int());
            AddColumn("dbo.ServiceIncidentalContracts", "IncidentalContracTypeId", c => c.Int());
            AddColumn("dbo.TrainningIncidentalContracts", "IncidentalContracTypeId", c => c.Int());
            CreateIndex("dbo.IncidentalContracts", "IncidentalContracTypeId");
            CreateIndex("dbo.ProductIncidentalContracts", "IncidentalContracTypeId");
            CreateIndex("dbo.ServiceIncidentalContracts", "IncidentalContracTypeId");
            CreateIndex("dbo.TrainningIncidentalContracts", "IncidentalContracTypeId");
            AddForeignKey("dbo.IncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes", "IncidentalContracTypeId");
            AddForeignKey("dbo.ProductIncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes", "IncidentalContracTypeId");
            AddForeignKey("dbo.ServiceIncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes", "IncidentalContracTypeId");
            AddForeignKey("dbo.TrainningIncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes", "IncidentalContracTypeId");
            DropColumn("dbo.IncidentalContracts", "IncidentalContracType");
            DropColumn("dbo.ProductIncidentalContracts", "IncidentalContracType");
            DropColumn("dbo.ServiceIncidentalContracts", "IncidentalContracType");
            DropColumn("dbo.TrainningIncidentalContracts", "IncidentalContracType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrainningIncidentalContracts", "IncidentalContracType", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceIncidentalContracts", "IncidentalContracType", c => c.Int(nullable: false));
            AddColumn("dbo.ProductIncidentalContracts", "IncidentalContracType", c => c.Int(nullable: false));
            AddColumn("dbo.IncidentalContracts", "IncidentalContracType", c => c.Int(nullable: false));
            DropForeignKey("dbo.TrainningIncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes");
            DropForeignKey("dbo.ServiceIncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes");
            DropForeignKey("dbo.ProductIncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes");
            DropForeignKey("dbo.IncidentalContracts", "IncidentalContracTypeId", "dbo.IncidentalContracTypes");
            DropIndex("dbo.TrainningIncidentalContracts", new[] { "IncidentalContracTypeId" });
            DropIndex("dbo.ServiceIncidentalContracts", new[] { "IncidentalContracTypeId" });
            DropIndex("dbo.ProductIncidentalContracts", new[] { "IncidentalContracTypeId" });
            DropIndex("dbo.IncidentalContracts", new[] { "IncidentalContracTypeId" });
            DropColumn("dbo.TrainningIncidentalContracts", "IncidentalContracTypeId");
            DropColumn("dbo.ServiceIncidentalContracts", "IncidentalContracTypeId");
            DropColumn("dbo.ProductIncidentalContracts", "IncidentalContracTypeId");
            DropColumn("dbo.IncidentalContracts", "IncidentalContracTypeId");
        }
    }
}
