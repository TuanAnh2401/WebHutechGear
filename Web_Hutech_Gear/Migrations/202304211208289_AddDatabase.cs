namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Adv", "Link", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.tb_News", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.tb_Posts", "Image", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Posts", "Image", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_News", "Image", c => c.String());
            AlterColumn("dbo.tb_Adv", "Link", c => c.String(maxLength: 500));
        }
    }
}
