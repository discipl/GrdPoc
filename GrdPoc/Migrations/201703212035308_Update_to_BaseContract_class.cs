namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_to_BaseContract_class : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductIncidentalContracts", "IncidentalContracDuration", c => c.Int());
            AlterColumn("dbo.ProductIncidentalContracts", "IncidentalContracTimeframeStart", c => c.DateTime());
            AlterColumn("dbo.ProductIncidentalContracts", "IncidentalContracTimeframeEnd", c => c.DateTime());
            AlterColumn("dbo.ProductIncidentalContracts", "OwnerTermsAgreementDate", c => c.DateTime());
            AlterColumn("dbo.ProductIncidentalContracts", "ProviderTermsAgreementDate", c => c.DateTime());
            AlterColumn("dbo.ProductIncidentalContracts", "ControllerDeliverenceConfirmationDate", c => c.DateTime());
            AlterColumn("dbo.ProductIncidentalContracts", "ProviderDeliverenceConfirmationDate", c => c.DateTime());
            AlterColumn("dbo.ServiceIncidentalContracts", "IncidentalContracDuration", c => c.Int());
            AlterColumn("dbo.ServiceIncidentalContracts", "IncidentalContracTimeframeStart", c => c.DateTime());
            AlterColumn("dbo.ServiceIncidentalContracts", "IncidentalContracTimeframeEnd", c => c.DateTime());
            AlterColumn("dbo.ServiceIncidentalContracts", "OwnerTermsAgreementDate", c => c.DateTime());
            AlterColumn("dbo.ServiceIncidentalContracts", "ProviderTermsAgreementDate", c => c.DateTime());
            AlterColumn("dbo.ServiceIncidentalContracts", "ControllerDeliverenceConfirmationDate", c => c.DateTime());
            AlterColumn("dbo.ServiceIncidentalContracts", "ProviderDeliverenceConfirmationDate", c => c.DateTime());
            AlterColumn("dbo.TrainningIncidentalContracts", "IncidentalContracDuration", c => c.Int());
            AlterColumn("dbo.TrainningIncidentalContracts", "IncidentalContracTimeframeStart", c => c.DateTime());
            AlterColumn("dbo.TrainningIncidentalContracts", "IncidentalContracTimeframeEnd", c => c.DateTime());
            AlterColumn("dbo.TrainningIncidentalContracts", "OwnerTermsAgreementDate", c => c.DateTime());
            AlterColumn("dbo.TrainningIncidentalContracts", "ProviderTermsAgreementDate", c => c.DateTime());
            AlterColumn("dbo.TrainningIncidentalContracts", "ControllerDeliverenceConfirmationDate", c => c.DateTime());
            AlterColumn("dbo.TrainningIncidentalContracts", "ProviderDeliverenceConfirmationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainningIncidentalContracts", "ProviderDeliverenceConfirmationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainningIncidentalContracts", "ControllerDeliverenceConfirmationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainningIncidentalContracts", "ProviderTermsAgreementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainningIncidentalContracts", "OwnerTermsAgreementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainningIncidentalContracts", "IncidentalContracTimeframeEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainningIncidentalContracts", "IncidentalContracTimeframeStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrainningIncidentalContracts", "IncidentalContracDuration", c => c.Int(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "ProviderDeliverenceConfirmationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "ControllerDeliverenceConfirmationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "ProviderTermsAgreementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "OwnerTermsAgreementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "IncidentalContracTimeframeEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "IncidentalContracTimeframeStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceIncidentalContracts", "IncidentalContracDuration", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "ProviderDeliverenceConfirmationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "ControllerDeliverenceConfirmationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "ProviderTermsAgreementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "OwnerTermsAgreementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "IncidentalContracTimeframeEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "IncidentalContracTimeframeStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductIncidentalContracts", "IncidentalContracDuration", c => c.Int(nullable: false));
        }
    }
}
