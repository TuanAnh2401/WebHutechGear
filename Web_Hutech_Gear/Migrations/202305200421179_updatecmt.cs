namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecmt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Comment", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Comment", "Rating", c => c.Int(nullable: false));
        }
    }
}
