namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Sale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coupons", "discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Coupons", "discountRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coupons", "discountRate", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.Coupons", "discount");
        }
    }
}
