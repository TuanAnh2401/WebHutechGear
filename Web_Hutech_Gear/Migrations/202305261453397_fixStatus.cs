namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixStatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Product", "StatusId", "dbo.tb_Status");
            DropIndex("dbo.tb_Product", new[] { "StatusId" });
            AddColumn("dbo.tb_Product", "IsStatus", c => c.Boolean(nullable: false));
            DropTable("dbo.tb_Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tb_Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsActivate = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.tb_Product", "IsStatus");
            CreateIndex("dbo.tb_Product", "StatusId");
            AddForeignKey("dbo.tb_Product", "StatusId", "dbo.tb_Status", "Id", cascadeDelete: true);
        }
    }
}
