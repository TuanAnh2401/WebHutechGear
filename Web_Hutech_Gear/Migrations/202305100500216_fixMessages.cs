namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.tb_Messages", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.tb_Messages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.tb_Messages", new[] { "ApplicationUser_Id1" });
            RenameColumn(table: "dbo.tb_Messages", name: "Receiver_Id", newName: "ReceiverId");
            RenameColumn(table: "dbo.tb_Messages", name: "Sender_Id", newName: "SenderId");
            RenameIndex(table: "dbo.tb_Messages", name: "IX_Sender_Id", newName: "IX_SenderId");
            RenameIndex(table: "dbo.tb_Messages", name: "IX_Receiver_Id", newName: "IX_ReceiverId");
            DropColumn("dbo.tb_Messages", "ApplicationUser_Id");
            DropColumn("dbo.tb_Messages", "ApplicationUser_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Messages", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.tb_Messages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            RenameIndex(table: "dbo.tb_Messages", name: "IX_ReceiverId", newName: "IX_Receiver_Id");
            RenameIndex(table: "dbo.tb_Messages", name: "IX_SenderId", newName: "IX_Sender_Id");
            RenameColumn(table: "dbo.tb_Messages", name: "SenderId", newName: "Sender_Id");
            RenameColumn(table: "dbo.tb_Messages", name: "ReceiverId", newName: "Receiver_Id");
            CreateIndex("dbo.tb_Messages", "ApplicationUser_Id1");
            CreateIndex("dbo.tb_Messages", "ApplicationUser_Id");
            AddForeignKey("dbo.tb_Messages", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.tb_Messages", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
