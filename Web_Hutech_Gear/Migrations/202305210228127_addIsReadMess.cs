namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsReadMess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Messages", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Messages", "IsRead");
        }
    }
}
