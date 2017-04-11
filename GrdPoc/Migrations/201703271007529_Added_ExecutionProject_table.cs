namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ExecutionProject_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExecutionProjects",
                c => new
                    {
                        ExecutionProjectId = c.Int(nullable: false, identity: true),
                        ExecutionProjectTitle = c.String(),
                        IncidentalContracId = c.Int(),
                        ExecutionProjectStatus = c.Int(nullable: false),
                        ExecutionProjectControllerId = c.Int(),
                        ExecutionProjectProviderId = c.Int(),
                        ExecutionProjectResourceId = c.Int(),
                        ExecutionProjectDuration = c.Int(),
                        ExecutionProjectTimeframeStart = c.DateTime(),
                        ExecutionProjectTimeframeEnd = c.DateTime(),
                        ExecutionProjectSchedulledStart = c.DateTime(),
                        ExecutionProjectSchedulledEnd = c.DateTime(),
                        ExecutionProjectActualStart = c.DateTime(),
                        ExecutionProjectActualEnd = c.DateTime(),
                        ExecutionProjectDeliveranceDate = c.DateTime(),
                        ExecutionProjectDeliveranceConfirmation = c.DateTime(),
                    })
                .PrimaryKey(t => t.ExecutionProjectId)
                .ForeignKey("dbo.UserAccounts", t => t.ExecutionProjectControllerId)
                .ForeignKey("dbo.UserAccounts", t => t.ExecutionProjectProviderId)
                .ForeignKey("dbo.UserAccounts", t => t.ExecutionProjectResourceId)
                .ForeignKey("dbo.IncidentalContracts", t => t.IncidentalContracId)
                .Index(t => t.IncidentalContracId)
                .Index(t => t.ExecutionProjectControllerId)
                .Index(t => t.ExecutionProjectProviderId)
                .Index(t => t.ExecutionProjectResourceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExecutionProjects", "IncidentalContracId", "dbo.IncidentalContracts");
            DropForeignKey("dbo.ExecutionProjects", "ExecutionProjectResourceId", "dbo.UserAccounts");
            DropForeignKey("dbo.ExecutionProjects", "ExecutionProjectProviderId", "dbo.UserAccounts");
            DropForeignKey("dbo.ExecutionProjects", "ExecutionProjectControllerId", "dbo.UserAccounts");
            DropIndex("dbo.ExecutionProjects", new[] { "ExecutionProjectResourceId" });
            DropIndex("dbo.ExecutionProjects", new[] { "ExecutionProjectProviderId" });
            DropIndex("dbo.ExecutionProjects", new[] { "ExecutionProjectControllerId" });
            DropIndex("dbo.ExecutionProjects", new[] { "IncidentalContracId" });
            DropTable("dbo.ExecutionProjects");
        }
    }
}
