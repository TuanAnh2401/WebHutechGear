namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "IsHome", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "IsSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Status", "Title", c => c.String());
            AddColumn("dbo.tb_Status", "CreatedBy", c => c.String());
            AddColumn("dbo.tb_Status", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Status", "Modifiedby", c => c.String());
            AddColumn("dbo.tb_Status", "ModifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.tb_Status", "IsHome");
            DropColumn("dbo.tb_Status", "IsSale");
            DropColumn("dbo.tb_Status", "IsHot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Status", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Status", "IsSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Status", "IsHome", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_Status", "ModifiedDate");
            DropColumn("dbo.tb_Status", "Modifiedby");
            DropColumn("dbo.tb_Status", "CreatedDate");
            DropColumn("dbo.tb_Status", "CreatedBy");
            DropColumn("dbo.tb_Status", "Title");
            DropColumn("dbo.tb_Product", "IsHot");
            DropColumn("dbo.tb_Product", "IsSale");
            DropColumn("dbo.tb_Product", "IsHome");
        }
    }
}
