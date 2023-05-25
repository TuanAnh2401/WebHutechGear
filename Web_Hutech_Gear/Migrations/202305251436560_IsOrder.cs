namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "IsOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "IsOrder");
        }
    }
}
