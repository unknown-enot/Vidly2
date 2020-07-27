namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenresToIdentity : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES(5, 'Thriller')");
        }
        
        public override void Down()
        {
        }
    }
}
