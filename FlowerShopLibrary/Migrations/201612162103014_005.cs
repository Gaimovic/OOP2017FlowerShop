namespace FlowerShopLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "FlowerDiscount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "FlowerDiscount");
        }
    }
}
