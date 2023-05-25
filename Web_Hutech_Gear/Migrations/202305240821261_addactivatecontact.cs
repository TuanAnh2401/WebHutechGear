namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addactivatecontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Contact", "IsActivate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Contact", "IsActivate");
        }
    }
}
