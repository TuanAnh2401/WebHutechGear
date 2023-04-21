namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Adv", "Image", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.tb_Product", "Detail", c => c.String(nullable: false));
            AlterColumn("dbo.tb_News", "Detail", c => c.String(nullable: false));
            AlterColumn("dbo.tb_Posts", "Detail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Posts", "Detail", c => c.String());
            AlterColumn("dbo.tb_News", "Detail", c => c.String());
            AlterColumn("dbo.tb_Product", "Detail", c => c.String());
            AlterColumn("dbo.tb_Adv", "Image", c => c.String(maxLength: 500));
        }
    }
}
