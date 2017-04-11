namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_added_status_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidentalContracts", "IncidentalContracStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncidentalContracts", "IncidentalContracStatus");
        }
    }
}
