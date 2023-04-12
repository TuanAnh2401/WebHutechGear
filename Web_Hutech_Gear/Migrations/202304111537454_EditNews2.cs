namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNews2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_News", "IsHome", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_News", "IsSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_News", "IsHot", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_News", "Alias");
            DropColumn("dbo.tb_News", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_News", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_News", "Alias", c => c.String());
            DropColumn("dbo.tb_News", "IsHot");
            DropColumn("dbo.tb_News", "IsSale");
            DropColumn("dbo.tb_News", "IsHome");
        }
    }
}
