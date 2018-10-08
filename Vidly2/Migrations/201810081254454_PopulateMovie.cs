namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovie : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies(Name, GenreId,ReleaseDate,NumberInStock) VALUES('Shrek',5,26/11/2007,7)");
            Sql("INSERT INTO Movies(Name, GenreId,ReleaseDate,NumberInStock) VALUES('Titanic',4,26/07/2001,13)");
        }
        
        public override void Down()
        {
        }
    }
}
