namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Comment", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_Comment", new[] { "ProductId" });
            AddColumn("dbo.tb_Comment", "NewsId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Comment", "Content", c => c.String(nullable: false));
            CreateIndex("dbo.tb_Comment", "NewsId");
            AddForeignKey("dbo.tb_Comment", "NewsId", "dbo.tb_News", "Id", cascadeDelete: true);
            DropColumn("dbo.tb_Comment", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Comment", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_Comment", "NewsId", "dbo.tb_News");
            DropIndex("dbo.tb_Comment", new[] { "NewsId" });
            AlterColumn("dbo.tb_Comment", "Content", c => c.String());
            DropColumn("dbo.tb_Comment", "NewsId");
            CreateIndex("dbo.tb_Comment", "ProductId");
            AddForeignKey("dbo.tb_Comment", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
        }
    }
}
