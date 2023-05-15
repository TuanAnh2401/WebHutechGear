namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatMessages : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tb_Messages", name: "UserId", newName: "ReceiverId");
            RenameIndex(table: "dbo.tb_Messages", name: "IX_UserId", newName: "IX_ReceiverId");
            AddColumn("dbo.tb_Messages", "SenderId", c => c.String(maxLength: 128));
            AddColumn("dbo.tb_Messages", "MessageText", c => c.String());
            AddColumn("dbo.tb_Messages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.tb_Messages", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.tb_Messages", "SenderId");
            CreateIndex("dbo.tb_Messages", "ApplicationUser_Id");
            CreateIndex("dbo.tb_Messages", "ApplicationUser_Id1");
            AddForeignKey("dbo.tb_Messages", "SenderId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.tb_Messages", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.tb_Messages", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.tb_Messages", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Messages", "Message", c => c.String());
            DropForeignKey("dbo.tb_Messages", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.tb_Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.tb_Messages", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.tb_Messages", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.tb_Messages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.tb_Messages", new[] { "SenderId" });
            DropColumn("dbo.tb_Messages", "ApplicationUser_Id1");
            DropColumn("dbo.tb_Messages", "ApplicationUser_Id");
            DropColumn("dbo.tb_Messages", "MessageText");
            DropColumn("dbo.tb_Messages", "SenderId");
            RenameIndex(table: "dbo.tb_Messages", name: "IX_ReceiverId", newName: "IX_UserId");
            RenameColumn(table: "dbo.tb_Messages", name: "ReceiverId", newName: "UserId");
        }
    }
}
