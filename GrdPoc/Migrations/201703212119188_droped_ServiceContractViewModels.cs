namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droped_ServiceContractViewModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncidentalContractItems", "IncidentalContractId", "dbo.ServiceContractViewModels");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracControllerId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracProviderId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracTypeId", "dbo.IncidentalContracTypes");
            DropForeignKey("dbo.ServiceContractViewModels", "OwnerMunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.ServiceContractViewModels", "IncidentalContracOwnerId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceContractViewModels", "ProviderMunicipalEntityId", "dbo.MunicipalEntities");
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracTypeId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "OwnerMunicipalEntityId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracOwnerId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracControllerId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "ProviderMunicipalEntityId" });
            DropIndex("dbo.ServiceContractViewModels", new[] { "IncidentalContracProviderId" });
            DropTable("dbo.ServiceContractViewModels");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.IncidentalContractId);
            
            CreateIndex("dbo.ServiceContractViewModels", "IncidentalContracProviderId");
            CreateIndex("dbo.ServiceContractViewModels", "ProviderMunicipalEntityId");
            CreateIndex("dbo.ServiceContractViewModels", "IncidentalContracControllerId");
            CreateIndex("dbo.ServiceContractViewModels", "IncidentalContracOwnerId");
            CreateIndex("dbo.ServiceContractViewModels", "OwnerMunicipalEntityId");
            CreateIndex("dbo.ServiceContractViewModels", "IncidentalContracTypeId");
            AddForeignKey("dbo.ServiceContractViewModels", "ProviderMunicipalEntityId", "dbo.MunicipalEntities", "MunicipalEntityId");
            AddForeignKey("dbo.ServiceContractViewModels", "IncidentalContracOwnerId", "dbo.UserAccounts", "UserAccountId");
            AddForeignKey("dbo.ServiceContractViewModels", "OwnerMunicipalEntityId", "dbo.MunicipalEntities", "MunicipalEntityId");
            AddForeignKey("dbo.ServiceContractViewModels", "IncidentalContracTypeId", "dbo.IncidentalContracTypes", "IncidentalContracTypeId");
            AddForeignKey("dbo.ServiceContractViewModels", "IncidentalContracProviderId", "dbo.UserAccounts", "UserAccountId");
            AddForeignKey("dbo.ServiceContractViewModels", "IncidentalContracControllerId", "dbo.UserAccounts", "UserAccountId");
            AddForeignKey("dbo.IncidentalContractItems", "IncidentalContractId", "dbo.ServiceContractViewModels", "IncidentalContractId");
        }
    }
}
