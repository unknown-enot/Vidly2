namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5bdbd6c4-ba76-4387-846d-6eae95e5a842', N'admin@vidly.com', 0, N'AJpHqczbadx2ySFl+VuZ987SmfMxoW+g0HWK50YcCP+dViKpqV0KvoztLcCPRLB+vA==', N'7ba62f34-b570-448a-b1b8-8b35db55e8ce', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6ecd7149-8bc9-430a-8136-179b51aea424', N'guest@vidly.com', 0, N'AFjwIEva+69M/ud7une+1NZxlaIft+0n+n5k2hqhiLTF5bCaqvEtEVAjgcqJJVNHAw==', N'baf7a9cd-e776-498d-ba4e-8d3427b34bd1', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6cb63cd0-70d8-4f6a-8ada-fd4ff8123eb5', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5bdbd6c4-ba76-4387-846d-6eae95e5a842', N'6cb63cd0-70d8-4f6a-8ada-fd4ff8123eb5')

");
        }
        
        public override void Down()
        {
        }
    }
}
