namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixreply : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Reply", "Comment_Id", "dbo.tb_Comment");
            DropForeignKey("dbo.tb_Comment", "Product_Id", "dbo.tb_Product");
            DropIndex("dbo.tb_Comment", new[] { "Product_Id" });
            DropIndex("dbo.tb_Reply", new[] { "Comment_Id" });
            RenameColumn(table: "dbo.tb_Comment", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.tb_Comment", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.tb_Reply", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.tb_Comment", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.tb_Reply", name: "IX_User_Id", newName: "IX_UserId");
            AddColumn("dbo.tb_Comment", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Reply", "RatedId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Comment", "Content", c => c.String());
            AlterColumn("dbo.tb_Comment", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Comment", "ProductId");
            CreateIndex("dbo.tb_Reply", "RatedId");
            AddForeignKey("dbo.tb_Reply", "RatedId", "dbo.tb_Rated", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_Comment", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
            DropColumn("dbo.tb_Reply", "Comment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Reply", "Comment_Id", c => c.Int());
            DropForeignKey("dbo.tb_Comment", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_Reply", "RatedId", "dbo.tb_Rated");
            DropIndex("dbo.tb_Reply", new[] { "RatedId" });
            DropIndex("dbo.tb_Comment", new[] { "ProductId" });
            AlterColumn("dbo.tb_Comment", "ProductId", c => c.Int());
            AlterColumn("dbo.tb_Comment", "Content", c => c.String(maxLength: 128));
            DropColumn("dbo.tb_Reply", "RatedId");
            DropColumn("dbo.tb_Comment", "Rating");
            RenameIndex(table: "dbo.tb_Reply", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.tb_Comment", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.tb_Reply", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.tb_Comment", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.tb_Comment", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.tb_Reply", "Comment_Id");
            CreateIndex("dbo.tb_Comment", "Product_Id");
            AddForeignKey("dbo.tb_Comment", "Product_Id", "dbo.tb_Product", "Id");
            AddForeignKey("dbo.tb_Reply", "Comment_Id", "dbo.tb_Comment", "Id");
        }
    }
}
