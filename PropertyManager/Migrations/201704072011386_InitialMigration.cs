namespace PropertyManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leases", "Apartment_ApartmentNumber", "dbo.Apartments");
            DropForeignKey("dbo.Leases", "Tenant_Id", "dbo.Tenants");
            DropIndex("dbo.Leases", new[] { "Apartment_ApartmentNumber" });
            DropIndex("dbo.Leases", new[] { "Tenant_Id" });
            AlterColumn("dbo.Leases", "Apartment_ApartmentNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Leases", "Tenant_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Leases", "Apartment_ApartmentNumber");
            CreateIndex("dbo.Leases", "Tenant_Id");
            AddForeignKey("dbo.Leases", "Apartment_ApartmentNumber", "dbo.Apartments", "ApartmentNumber", cascadeDelete: true);
            AddForeignKey("dbo.Leases", "Tenant_Id", "dbo.Tenants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leases", "Tenant_Id", "dbo.Tenants");
            DropForeignKey("dbo.Leases", "Apartment_ApartmentNumber", "dbo.Apartments");
            DropIndex("dbo.Leases", new[] { "Tenant_Id" });
            DropIndex("dbo.Leases", new[] { "Apartment_ApartmentNumber" });
            AlterColumn("dbo.Leases", "Tenant_Id", c => c.Int());
            AlterColumn("dbo.Leases", "Apartment_ApartmentNumber", c => c.Int());
            CreateIndex("dbo.Leases", "Tenant_Id");
            CreateIndex("dbo.Leases", "Apartment_ApartmentNumber");
            AddForeignKey("dbo.Leases", "Tenant_Id", "dbo.Tenants", "Id");
            AddForeignKey("dbo.Leases", "Apartment_ApartmentNumber", "dbo.Apartments", "ApartmentNumber");
        }
    }
}
