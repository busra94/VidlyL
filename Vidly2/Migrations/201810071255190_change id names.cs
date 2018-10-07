namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeidnames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Customers", "MembershipTypeId");
            AddForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "MembershipType_MembershipId");
            DropColumn("dbo.Customers", "MembershipType_SignUpFee");
            DropColumn("dbo.Customers", "MembershipType_DurationInMonths");
            DropColumn("dbo.Customers", "MembershipType_DiscountRate");
            DropColumn("dbo.Customers", "MembershipTypeMembershipId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MembershipTypeMembershipId", c => c.Byte(nullable: false));
            AddColumn("dbo.Customers", "MembershipType_DiscountRate", c => c.Byte(nullable: false));
            AddColumn("dbo.Customers", "MembershipType_DurationInMonths", c => c.Byte(nullable: false));
            AddColumn("dbo.Customers", "MembershipType_SignUpFee", c => c.Short(nullable: false));
            AddColumn("dbo.Customers", "MembershipType_MembershipId", c => c.Byte(nullable: false));
            DropForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershipTypeId" });
            DropColumn("dbo.Customers", "MembershipTypeId");
            DropTable("dbo.MembershipTypes");
        }
    }
}
