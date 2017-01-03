namespace FlowerShopLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _006 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "FlowerDiscount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "FlowerDiscount", c => c.Int());
        }
    }
}
