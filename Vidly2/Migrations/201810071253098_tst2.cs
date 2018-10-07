namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tst2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "MembershipTypeMembershipId", c => c.Byte(nullable: false));
            DropColumn("dbo.Customers", "MembershipTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));
            DropColumn("dbo.Customers", "MembershipTypeMembershipId");
        }
    }
}
