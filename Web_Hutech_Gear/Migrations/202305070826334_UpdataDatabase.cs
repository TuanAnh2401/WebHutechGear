namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        Product_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.Product_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.tb_Reply",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        Comment_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Comment", t => t.Comment_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Reply", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.tb_Reply", "Comment_Id", "dbo.tb_Comment");
            DropForeignKey("dbo.tb_Comment", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.tb_Comment", "Product_Id", "dbo.tb_Product");
            DropIndex("dbo.tb_Reply", new[] { "User_Id" });
            DropIndex("dbo.tb_Reply", new[] { "Comment_Id" });
            DropIndex("dbo.tb_Comment", new[] { "User_Id" });
            DropIndex("dbo.tb_Comment", new[] { "Product_Id" });
            DropTable("dbo.tb_Reply");
            DropTable("dbo.tb_Comment");
        }
    }
}
