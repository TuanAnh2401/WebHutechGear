namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Product", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "StatusId", c => c.Int(nullable: false));
        }
    }
}
