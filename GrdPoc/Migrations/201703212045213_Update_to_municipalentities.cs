namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_to_municipalentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceContractViewModels",
                c => new
                    {
                        IncidentalContractId = c.Int(nullable: false, identity: true),
                        ContractTitle = c.String(),
                        IncidentalContracTypeId = c.Int(),
                        OwnerMunicipalEntityId = c.Int(),
                        IncidentalContracOwnerId = c.Int(),
                        IncidentalContracOwnerName = c.String(),
                        IncidentalContracControllerId = c.Int(),
                        IncidentalContracControllerName = c.String(),
                        ProviderMunicipalEntityId = c.Int(),
                        IncidentalContracProviderId = c.Int(),
                        IncidentalContracProviderName = c.String(),
                        IncidentalContracInvoiceNumber = c.String(),
                        IncidentalContracStatus = c.Int(nullable: false),
                        IncidentalContracDuration = c.Int(),
                        IncidentalContracTimeframeStart = c.DateTime(),
                        IncidentalContracTimeframeEnd = c.DateTime(),
                        OwnerTermsAgreementDate = c.DateTime(),
                        ProviderTermsAgreementDate = c.DateTime(),
                        ControllerDeliverenceConfirmationDate = c.DateTime(),
                        ProviderDeliverenceConfirmationDate = c.DateTime(),
                        ServiceIncidentalContractId = c.Int(nullable: false),
                        ServiceIncidentalContractPurpose = c.String(),
                        ServiceIncidentalContractHours = c.Int(nullable: false),
                        ServiceIncidentalContractProfile = c.String(),
                    })
                .PrimaryKey(t => t.IncidentalContractId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracControllerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracProviderId)
                .ForeignKey("dbo.IncidentalContracTypes", t => t.IncidentalContracTypeId)
                .ForeignKey("dbo.MunicipalEntities", t => t.OwnerMunicipalEntityId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracOwnerId)
                .ForeignKey("dbo.MunicipalEntities", t => t.ProviderMunicipalEntityId)
                .Index(t => t.IncidentalContracTypeId)
                .Index(t => t.OwnerMunicipalEntityId)
                .Index(t => t.IncidentalContracOwnerId)
                .Index(t => t.IncidentalContracControllerId)
                .Index(t => t.ProviderMunicipalEntityId)
                .Index(t => t.IncidentalContracProviderId);
            
            AddForeignKey("dbo.IncidentalContractItems", "IncidentalContractId", "dbo.ServiceContractViewModels", "IncidentalContractId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceContractViewModels", "ProviderMunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracOwnerId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceContractViewModels", "OwnerMunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracTypeId", "dbo.IncidentalContracTypes");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracProviderId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracControllerId", "dbo.UserAccounts");
            DropForeignKey("dbo.IncidentalContractItems", "IncidentalContractId", "dbo.ServiceContractViewModels");
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracProviderId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "ProviderMunicipalEntityId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracControllerId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracOwnerId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "OwnerMunicipalEntityId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracTypeId" });
            DropTable("dbo.ServiceContractViewModels");
        }
    }
}
