namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers (Name,IsSubscribedToNewsLetter, MembershipTypeId) VALUES ('John Smith', 0, 1)");
            Sql("INSERT INTO Customers (Name,IsSubscribedToNewsLetter, MembershipTypeId) VALUES ('Mary Williams', 1, 2)");
        }
        
        public override void Down()
        {
        }
    }
}
