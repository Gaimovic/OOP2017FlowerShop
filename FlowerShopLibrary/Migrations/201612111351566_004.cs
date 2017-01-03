namespace FlowerShopLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _004 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "NewsAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.News", "ModifiedDateTime", c => c.DateTime());
            AddColumn("dbo.News", "ModifiedById", c => c.Int());
            CreateIndex("dbo.News", "CreatedById");
            CreateIndex("dbo.News", "ModifiedById");
            AddForeignKey("dbo.News", "CreatedById", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.News", "ModifiedById", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.News", "CreatedById", "dbo.User");
            DropIndex("dbo.News", new[] { "ModifiedById" });
            DropIndex("dbo.News", new[] { "CreatedById" });
            DropColumn("dbo.News", "ModifiedById");
            DropColumn("dbo.News", "ModifiedDateTime");
            DropColumn("dbo.News", "CreatedById");
            DropColumn("dbo.News", "NewsAdded");
        }
    }
}
