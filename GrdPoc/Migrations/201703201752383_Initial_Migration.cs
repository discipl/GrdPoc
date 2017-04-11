namespace GrdPoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockchainAccounts",
                c => new
                    {
                        BlockChainAccountId = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        AccountAddress = c.String(),
                        AccountPassword = c.String(),
                        AccountType = c.String(),
                        AccountActive = c.Boolean(nullable: false),
                        PreferedBlockChainNodeServerId = c.Int(),
                    })
                .PrimaryKey(t => t.BlockChainAccountId)
                .ForeignKey("dbo.BlockchainNodeServers", t => t.PreferedBlockChainNodeServerId)
                .Index(t => t.PreferedBlockChainNodeServerId);
            
            CreateTable(
                "dbo.BlockchainNodeServers",
                c => new
                    {
                        BlockChainNodeServerId = c.Int(nullable: false, identity: true),
                        NodeServerName = c.String(),
                        NodeServerNameAddress = c.String(),
                        NodeServerIpAddress = c.String(),
                        NodeServerActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BlockChainNodeServerId);
            
            CreateTable(
                "dbo.BudgetBalanceTransactions",
                c => new
                    {
                        BudgetBalanceTransactionId = c.Int(nullable: false, identity: true),
                        BudgetBalanceDeposit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetBalanceDepositSource = c.String(),
                        BudgetBalanceWithdrawal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetBalanceWithdrawalSource = c.String(),
                    })
                .PrimaryKey(t => t.BudgetBalanceTransactionId);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        BudgetName = c.String(),
                        BudgetIdCode = c.String(),
                        BudgetBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetOwnerId = c.Int(),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.UserAccounts", t => t.BudgetOwnerId)
                .Index(t => t.BudgetOwnerId);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserAccountId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                        UserTitle = c.String(),
                        PersonaTypeId = c.Int(),
                        MunicipalEntityId = c.Int(),
                    })
                .PrimaryKey(t => t.UserAccountId)
                .ForeignKey("dbo.MunicipalEntities", t => t.MunicipalEntityId)
                .ForeignKey("dbo.PersonaTypes", t => t.PersonaTypeId)
                .Index(t => t.PersonaTypeId)
                .Index(t => t.MunicipalEntityId);
            
            CreateTable(
                "dbo.MunicipalEntities",
                c => new
                    {
                        MunicipalEntityId = c.Int(nullable: false, identity: true),
                        MunicipalEntityName = c.String(),
                        MunicipalEntityAddress = c.String(),
                        MunicipalEntityKvk = c.String(),
                        MunicipalEntityBtw = c.String(),
                        MunicipalEntityIban = c.String(),
                        MunicipalEntityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MunicipalEntityId);
            
            CreateTable(
                "dbo.IncidentalContracts",
                c => new
                    {
                        IncidentalContractId = c.Int(nullable: false, identity: true),
                        ContractTitle = c.String(),
                        IncidentalContracType = c.Int(nullable: false),
                        OwnerMunicipalEntityId = c.Int(),
                        IncidentalContracOwnerId = c.Int(),
                        IncidentalContracOwnerName = c.String(),
                        IncidentalContracControllerId = c.Int(),
                        IncidentalContracControllerName = c.String(),
                        ProviderMunicipalEntityId = c.Int(),
                        IncidentalContracProviderId = c.Int(),
                        IncidentalContracProviderName = c.String(),
                        IncidentalContracInvoiceNumber = c.String(),
                        MunicipalEntity_MunicipalEntityId = c.Int(),
                        MunicipalEntity_MunicipalEntityId1 = c.Int(),
                    })
                .PrimaryKey(t => t.IncidentalContractId)
                .ForeignKey("dbo.MunicipalEntities", t => t.OwnerMunicipalEntityId)
                .ForeignKey("dbo.MunicipalEntities", t => t.ProviderMunicipalEntityId)
                .ForeignKey("dbo.MunicipalEntities", t => t.MunicipalEntity_MunicipalEntityId)
                .ForeignKey("dbo.MunicipalEntities", t => t.MunicipalEntity_MunicipalEntityId1)
                .Index(t => t.OwnerMunicipalEntityId)
                .Index(t => t.ProviderMunicipalEntityId)
                .Index(t => t.MunicipalEntity_MunicipalEntityId)
                .Index(t => t.MunicipalEntity_MunicipalEntityId1);
            
            CreateTable(
                "dbo.IncidentalContractItems",
                c => new
                    {
                        IncidentalContractItemId = c.Int(nullable: false, identity: true),
                        IncidentalContractItemDescription = c.String(),
                        IncidentalContractItemValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncidentalContractItemQuantity = c.Int(nullable: false),
                        IncidentalContractItemVat = c.Int(nullable: false),
                        ProductIncomeCode = c.String(),
                        ProductIncomeCodeId = c.Int(),
                        ProductExpenseCode = c.String(),
                        ProductExpenseCodeId = c.Int(),
                        BudgetCodeId = c.String(),
                        OwnerBudgetId = c.Int(),
                        IncidentalContractId = c.Int(),
                    })
                .PrimaryKey(t => t.IncidentalContractItemId)
                .ForeignKey("dbo.IncidentalContracts", t => t.IncidentalContractId)
                .ForeignKey("dbo.Budgets", t => t.OwnerBudgetId)
                .ForeignKey("dbo.ProductCodes", t => t.ProductExpenseCodeId)
                .ForeignKey("dbo.ProductCodes", t => t.ProductIncomeCodeId)
                .Index(t => t.ProductIncomeCodeId)
                .Index(t => t.ProductExpenseCodeId)
                .Index(t => t.OwnerBudgetId)
                .Index(t => t.IncidentalContractId);
            
            CreateTable(
                "dbo.ProductCodes",
                c => new
                    {
                        ProductCodeId = c.Int(nullable: false, identity: true),
                        ProductCodeNumber = c.String(),
                        ProductCodeType = c.String(),
                        ProductCodeDescription = c.String(),
                        ProductCodeCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductCodeId)
                .ForeignKey("dbo.ProductCodeCategories", t => t.ProductCodeCategoryId)
                .Index(t => t.ProductCodeCategoryId);
            
            CreateTable(
                "dbo.ProductCodeCategories",
                c => new
                    {
                        ProductCodeCategoryId = c.Int(nullable: false, identity: true),
                        ProductCodeCategoryNumber = c.Int(nullable: false),
                        ProductCodeCategoryDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProductCodeCategoryId);
            
            CreateTable(
                "dbo.ProductIncidentalContracts",
                c => new
                    {
                        TrainningIncidentalContractId = c.Int(nullable: false, identity: true),
                        IncidentalContractId = c.Int(),
                        IncidentalContractTitle = c.String(),
                        IncidentalContracType = c.Int(nullable: false),
                        IncidentalContracStatus = c.Int(nullable: false),
                        IncidentalContracOwnerId = c.Int(),
                        IncidentalContracControllerId = c.Int(),
                        IncidentalContracProviderId = c.Int(),
                        IncidentalContracDuration = c.Int(nullable: false),
                        IncidentalContracTimeframeStart = c.DateTime(nullable: false),
                        IncidentalContracTimeframeEnd = c.DateTime(nullable: false),
                        OwnerTermsAgreementDate = c.DateTime(nullable: false),
                        ProviderTermsAgreementDate = c.DateTime(nullable: false),
                        ControllerDeliverenceConfirmationDate = c.DateTime(nullable: false),
                        ProviderDeliverenceConfirmationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TrainningIncidentalContractId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracControllerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracOwnerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracProviderId)
                .ForeignKey("dbo.IncidentalContracts", t => t.IncidentalContractId)
                .Index(t => t.IncidentalContractId)
                .Index(t => t.IncidentalContracOwnerId)
                .Index(t => t.IncidentalContracControllerId)
                .Index(t => t.IncidentalContracProviderId);
            
            CreateTable(
                "dbo.ServiceIncidentalContracts",
                c => new
                    {
                        ServiceIncidentalContractId = c.Int(nullable: false, identity: true),
                        ServiceIncidentalContractPurpose = c.String(),
                        ServiceIncidentalContractHours = c.Int(nullable: false),
                        ServiceIncidentalContractProfile = c.String(),
                        IncidentalContractId = c.Int(),
                        IncidentalContractTitle = c.String(),
                        IncidentalContracType = c.Int(nullable: false),
                        IncidentalContracStatus = c.Int(nullable: false),
                        IncidentalContracOwnerId = c.Int(),
                        IncidentalContracControllerId = c.Int(),
                        IncidentalContracProviderId = c.Int(),
                        IncidentalContracDuration = c.Int(nullable: false),
                        IncidentalContracTimeframeStart = c.DateTime(nullable: false),
                        IncidentalContracTimeframeEnd = c.DateTime(nullable: false),
                        OwnerTermsAgreementDate = c.DateTime(nullable: false),
                        ProviderTermsAgreementDate = c.DateTime(nullable: false),
                        ControllerDeliverenceConfirmationDate = c.DateTime(nullable: false),
                        ProviderDeliverenceConfirmationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceIncidentalContractId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracControllerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracOwnerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracProviderId)
                .ForeignKey("dbo.IncidentalContracts", t => t.IncidentalContractId)
                .Index(t => t.IncidentalContractId)
                .Index(t => t.IncidentalContracOwnerId)
                .Index(t => t.IncidentalContracControllerId)
                .Index(t => t.IncidentalContracProviderId);
            
            CreateTable(
                "dbo.TrainningIncidentalContracts",
                c => new
                    {
                        TrainningIncidentalContractId = c.Int(nullable: false, identity: true),
                        ServiceIncidentalContractPurpose = c.String(),
                        ServiceIncidentalContractHours = c.Int(nullable: false),
                        ServiceIncidentalContractProfile = c.String(),
                        IncidentalContractId = c.Int(),
                        IncidentalContractTitle = c.String(),
                        IncidentalContracType = c.Int(nullable: false),
                        IncidentalContracStatus = c.Int(nullable: false),
                        IncidentalContracOwnerId = c.Int(),
                        IncidentalContracControllerId = c.Int(),
                        IncidentalContracProviderId = c.Int(),
                        IncidentalContracDuration = c.Int(nullable: false),
                        IncidentalContracTimeframeStart = c.DateTime(nullable: false),
                        IncidentalContracTimeframeEnd = c.DateTime(nullable: false),
                        OwnerTermsAgreementDate = c.DateTime(nullable: false),
                        ProviderTermsAgreementDate = c.DateTime(nullable: false),
                        ControllerDeliverenceConfirmationDate = c.DateTime(nullable: false),
                        ProviderDeliverenceConfirmationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TrainningIncidentalContractId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracControllerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracOwnerId)
                .ForeignKey("dbo.UserAccounts", t => t.IncidentalContracProviderId)
                .ForeignKey("dbo.IncidentalContracts", t => t.IncidentalContractId)
                .Index(t => t.IncidentalContractId)
                .Index(t => t.IncidentalContracOwnerId)
                .Index(t => t.IncidentalContracControllerId)
                .Index(t => t.IncidentalContracProviderId);
            
            CreateTable(
                "dbo.PersonaTypes",
                c => new
                    {
                        PersonaTypeId = c.Int(nullable: false, identity: true),
                        PersonaTypeName = c.String(),
                    })
                .PrimaryKey(t => t.PersonaTypeId);
            
            CreateTable(
                "dbo.IncidentalContracTypes",
                c => new
                    {
                        IncidentalContracTypeId = c.Int(nullable: false, identity: true),
                        IncidentalContracTypeTitle = c.String(),
                        IncidentalContracDescription = c.String(),
                        IncidentalContracMvcController = c.String(),
                        SmartContractId = c.Int(),
                        IncidentalContracTypeActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IncidentalContracTypeId)
                .ForeignKey("dbo.SmartContracts", t => t.SmartContractId)
                .Index(t => t.SmartContractId);
            
            CreateTable(
                "dbo.SmartContracts",
                c => new
                    {
                        SmartContractId = c.Int(nullable: false, identity: true),
                        SmartContractName = c.String(),
                        SmartContractByteCode = c.String(),
                        SmartContractAbi = c.String(),
                        SmartContractAddress = c.String(),
                        SmartContractCode = c.String(),
                        SmartContractActive = c.Boolean(nullable: false),
                        BlockChainAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.SmartContractId)
                .ForeignKey("dbo.BlockchainAccounts", t => t.BlockChainAccountId)
                .Index(t => t.BlockChainAccountId);
            
            CreateTable(
                "dbo.MunicipalEntityTypes",
                c => new
                    {
                        MunicipalEntityTypeId = c.Int(nullable: false, identity: true),
                        MunicipalEntityName = c.String(),
                    })
                .PrimaryKey(t => t.MunicipalEntityTypeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TrainningAttendees",
                c => new
                    {
                        TrainningAttendeeId = c.Int(nullable: false, identity: true),
                        TrainningAttendeeName = c.String(),
                        TrainningAttendeeTitle = c.String(),
                        UserAccountId = c.Int(),
                        TrainningIncidentalContractId = c.Int(),
                    })
                .PrimaryKey(t => t.TrainningAttendeeId)
                .ForeignKey("dbo.TrainningIncidentalContracts", t => t.TrainningIncidentalContractId)
                .ForeignKey("dbo.UserAccounts", t => t.UserAccountId)
                .Index(t => t.UserAccountId)
                .Index(t => t.TrainningIncidentalContractId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrainningAttendees", "UserAccountId", "dbo.UserAccounts");
            DropForeignKey("dbo.TrainningAttendees", "TrainningIncidentalContractId", "dbo.TrainningIncidentalContracts");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IncidentalContracTypes", "SmartContractId", "dbo.SmartContracts");
            DropForeignKey("dbo.SmartContracts", "BlockChainAccountId", "dbo.BlockchainAccounts");
            DropForeignKey("dbo.Budgets", "BudgetOwnerId", "dbo.UserAccounts");
            DropForeignKey("dbo.UserAccounts", "PersonaTypeId", "dbo.PersonaTypes");
            DropForeignKey("dbo.UserAccounts", "MunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.IncidentalContracts", "MunicipalEntity_MunicipalEntityId1", "dbo.MunicipalEntities");
            DropForeignKey("dbo.IncidentalContracts", "MunicipalEntity_MunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.TrainningIncidentalContracts", "IncidentalContractId", "dbo.IncidentalContracts");
            DropForeignKey("dbo.TrainningIncidentalContracts", "IncidentalContracProviderId", "dbo.UserAccounts");
            DropForeignKey("dbo.TrainningIncidentalContracts", "IncidentalContracOwnerId", "dbo.UserAccounts");
            DropForeignKey("dbo.TrainningIncidentalContracts", "IncidentalContracControllerId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceIncidentalContracts", "IncidentalContractId", "dbo.IncidentalContracts");
            DropForeignKey("dbo.ServiceIncidentalContracts", "IncidentalContracProviderId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceIncidentalContracts", "IncidentalContracOwnerId", "dbo.UserAccounts");
            DropForeignKey("dbo.ServiceIncidentalContracts", "IncidentalContracControllerId", "dbo.UserAccounts");
            DropForeignKey("dbo.IncidentalContracts", "ProviderMunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.ProductIncidentalContracts", "IncidentalContractId", "dbo.IncidentalContracts");
            DropForeignKey("dbo.ProductIncidentalContracts", "IncidentalContracProviderId", "dbo.UserAccounts");
            DropForeignKey("dbo.ProductIncidentalContracts", "IncidentalContracOwnerId", "dbo.UserAccounts");
            DropForeignKey("dbo.ProductIncidentalContracts", "IncidentalContracControllerId", "dbo.UserAccounts");
            DropForeignKey("dbo.IncidentalContracts", "OwnerMunicipalEntityId", "dbo.MunicipalEntities");
            DropForeignKey("dbo.IncidentalContractItems", "ProductIncomeCodeId", "dbo.ProductCodes");
            DropForeignKey("dbo.IncidentalContractItems", "ProductExpenseCodeId", "dbo.ProductCodes");
            DropForeignKey("dbo.ProductCodes", "ProductCodeCategoryId", "dbo.ProductCodeCategories");
            DropForeignKey("dbo.IncidentalContractItems", "OwnerBudgetId", "dbo.Budgets");
            DropForeignKey("dbo.IncidentalContractItems", "IncidentalContractId", "dbo.IncidentalContracts");
            DropForeignKey("dbo.BlockchainAccounts", "PreferedBlockChainNodeServerId", "dbo.BlockchainNodeServers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TrainningAttendees", new[] { "TrainningIncidentalContractId" });
            DropIndex("dbo.TrainningAttendees", new[] { "UserAccountId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SmartContracts", new[] { "BlockChainAccountId" });
            DropIndex("dbo.IncidentalContracTypes", new[] { "SmartContractId" });
            DropIndex("dbo.TrainningIncidentalContracts", new[] { "IncidentalContracProviderId" });
            DropIndex("dbo.TrainningIncidentalContracts", new[] { "IncidentalContracControllerId" });
            DropIndex("dbo.TrainningIncidentalContracts", new[] { "IncidentalContracOwnerId" });
            DropIndex("dbo.TrainningIncidentalContracts", new[] { "IncidentalContractId" });
            DropIndex("dbo.ServiceIncidentalContracts", new[] { "IncidentalContracProviderId" });
            DropIndex("dbo.ServiceIncidentalContracts", new[] { "IncidentalContracControllerId" });
            DropIndex("dbo.ServiceIncidentalContracts", new[] { "IncidentalContracOwnerId" });
            DropIndex("dbo.ServiceIncidentalContracts", new[] { "IncidentalContractId" });
            DropIndex("dbo.ProductIncidentalContracts", new[] { "IncidentalContracProviderId" });
            DropIndex("dbo.ProductIncidentalContracts", new[] { "IncidentalContracControllerId" });
            DropIndex("dbo.ProductIncidentalContracts", new[] { "IncidentalContracOwnerId" });
            DropIndex("dbo.ProductIncidentalContracts", new[] { "IncidentalContractId" });
            DropIndex("dbo.ProductCodes", new[] { "ProductCodeCategoryId" });
            DropIndex("dbo.IncidentalContractItems", new[] { "IncidentalContractId" });
            DropIndex("dbo.IncidentalContractItems", new[] { "OwnerBudgetId" });
            DropIndex("dbo.IncidentalContractItems", new[] { "ProductExpenseCodeId" });
            DropIndex("dbo.IncidentalContractItems", new[] { "ProductIncomeCodeId" });
            DropIndex("dbo.IncidentalContracts", new[] { "MunicipalEntity_MunicipalEntityId1" });
            DropIndex("dbo.IncidentalContracts", new[] { "MunicipalEntity_MunicipalEntityId" });
            DropIndex("dbo.IncidentalContracts", new[] { "ProviderMunicipalEntityId" });
            DropIndex("dbo.IncidentalContracts", new[] { "OwnerMunicipalEntityId" });
            DropIndex("dbo.UserAccounts", new[] { "MunicipalEntityId" });
            DropIndex("dbo.UserAccounts", new[] { "PersonaTypeId" });
            DropIndex("dbo.Budgets", new[] { "BudgetOwnerId" });
            DropIndex("dbo.BlockchainAccounts", new[] { "PreferedBlockChainNodeServerId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TrainningAttendees");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MunicipalEntityTypes");
            DropTable("dbo.SmartContracts");
            DropTable("dbo.IncidentalContracTypes");
            DropTable("dbo.PersonaTypes");
            DropTable("dbo.TrainningIncidentalContracts");
            DropTable("dbo.ServiceIncidentalContracts");
            DropTable("dbo.ProductIncidentalContracts");
            DropTable("dbo.ProductCodeCategories");
            DropTable("dbo.ProductCodes");
            DropTable("dbo.IncidentalContractItems");
            DropTable("dbo.IncidentalContracts");
            DropTable("dbo.MunicipalEntities");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Budgets");
            DropTable("dbo.BudgetBalanceTransactions");
            DropTable("dbo.BlockchainNodeServers");
            DropTable("dbo.BlockchainAccounts");
        }
    }
}
