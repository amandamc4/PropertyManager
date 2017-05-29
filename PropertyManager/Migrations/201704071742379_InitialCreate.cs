namespace PropertyManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        ApartmentNumber = c.Int(nullable: false),
                        FloorNumber = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ApartmentNumber)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bedrooms = c.Int(nullable: false),
                        Bathrooms = c.Int(nullable: false),
                        SquareFeet = c.Double(nullable: false),
                        MaxOccupants = c.Int(),
                        Balcony = c.Boolean(nullable: false),
                        Dishwasher = c.Boolean(nullable: false),
                        Laundry = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PathName = c.String(),
                        Description = c.String(),
                        Unit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.Unit_Id, cascadeDelete: true)
                .Index(t => t.Unit_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        Title = c.String(maxLength: 30),
                        BirthDate = c.DateTime(),
                        HireDate = c.DateTime(),
                        Address = c.String(maxLength: 70),
                        City = c.String(maxLength: 40),
                        State = c.String(maxLength: 40),
                        Country = c.String(maxLength: 40),
                        PostalCode = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 24),
                        Email = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacilityName = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                        Status = c.String(),
                        OpenTime = c.DateTime(),
                        CloseTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacilityBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookedDate = c.DateTime(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        Notes = c.String(),
                        Facility_Id = c.Int(nullable: false),
                        Tenant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facilities", t => t.Facility_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tenants", t => t.Tenant_Id, cascadeDelete: true)
                .Index(t => t.Facility_Id)
                .Index(t => t.Tenant_Id);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MobilePhone = c.String(),
                        HomePhone = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        ActivationCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Supplier = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Leases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        SecurityDeposit = c.Double(),
                        MonthlyRent = c.Double(nullable: false),
                        Apartment_ApartmentNumber = c.Int(),
                        Tenant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.Apartment_ApartmentNumber)
                .ForeignKey("dbo.Tenants", t => t.Tenant_Id)
                .Index(t => t.Apartment_ApartmentNumber)
                .Index(t => t.Tenant_Id);
            
            CreateTable(
                "dbo.Occupants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MobilePhone = c.String(),
                        WorkPhone = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(),
                        ApartmentNumber = c.Int(nullable: false),
                        Tenant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenants", t => t.Tenant_Id, cascadeDelete: true)
                .Index(t => t.Tenant_Id);
            
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
                "dbo.ServiceRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        RequestDate = c.DateTime(),
                        CompletionDate = c.DateTime(),
                        Notes = c.String(),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        CompanyName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GivenName = c.String(),
                        Surname = c.String(),
                        Role = c.String(),
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
            
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Notes = c.String(),
                        RequestDate = c.DateTime(),
                        CompletionDate = c.DateTime(),
                        Tenant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenants", t => t.Tenant_Id, cascadeDelete: true)
                .Index(t => t.Tenant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrders", "Tenant_Id", "dbo.Tenants");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceRequests", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Occupants", "Tenant_Id", "dbo.Tenants");
            DropForeignKey("dbo.Leases", "Tenant_Id", "dbo.Tenants");
            DropForeignKey("dbo.Leases", "Apartment_ApartmentNumber", "dbo.Apartments");
            DropForeignKey("dbo.FacilityBookings", "Tenant_Id", "dbo.Tenants");
            DropForeignKey("dbo.FacilityBookings", "Facility_Id", "dbo.Facilities");
            DropForeignKey("dbo.UnitPhotoes", "Unit_Id", "dbo.Units");
            DropForeignKey("dbo.Apartments", "UnitId", "dbo.Units");
            DropIndex("dbo.WorkOrders", new[] { "Tenant_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ServiceRequests", new[] { "Service_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Occupants", new[] { "Tenant_Id" });
            DropIndex("dbo.Leases", new[] { "Tenant_Id" });
            DropIndex("dbo.Leases", new[] { "Apartment_ApartmentNumber" });
            DropIndex("dbo.FacilityBookings", new[] { "Tenant_Id" });
            DropIndex("dbo.FacilityBookings", new[] { "Facility_Id" });
            DropIndex("dbo.UnitPhotoes", new[] { "Unit_Id" });
            DropIndex("dbo.Apartments", new[] { "UnitId" });
            DropTable("dbo.WorkOrders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceRequests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Occupants");
            DropTable("dbo.Leases");
            DropTable("dbo.Inventories");
            DropTable("dbo.Tenants");
            DropTable("dbo.FacilityBookings");
            DropTable("dbo.Facilities");
            DropTable("dbo.Employees");
            DropTable("dbo.UnitPhotoes");
            DropTable("dbo.Units");
            DropTable("dbo.Apartments");
            DropTable("dbo.Announcements");
        }
    }
}
