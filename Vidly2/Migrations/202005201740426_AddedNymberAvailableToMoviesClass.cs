namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNymberAvailableToMoviesClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Byte(nullable: false));
            AlterColumn("dbo.Movies", "NumberInStock", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
