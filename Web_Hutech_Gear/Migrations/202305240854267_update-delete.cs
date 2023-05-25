namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_ProductImage", "IsActivate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductImage", "IsActivate", c => c.Boolean(nullable: false));
        }
    }
}
