namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_SubComment", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_SubComment", "ProductId");
        }
    }
}
