namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Rating", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_Rating", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.tb_Rating", new[] { "UserId" });
            DropIndex("dbo.tb_Rating", new[] { "ProductId" });
            AddColumn("dbo.tb_Comment", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Comment", "CreatedBy");
            DropColumn("dbo.tb_Comment", "Modifiedby");
            DropColumn("dbo.tb_Comment", "ModifiedDate");
            DropTable("dbo.tb_Rating");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tb_Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Comment", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Comment", "Modifiedby", c => c.String());
            AddColumn("dbo.tb_Comment", "CreatedBy", c => c.String());
            DropColumn("dbo.tb_Comment", "Rating");
            CreateIndex("dbo.tb_Rating", "ProductId");
            CreateIndex("dbo.tb_Rating", "UserId");
            AddForeignKey("dbo.tb_Rating", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.tb_Rating", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
        }
    }
}
