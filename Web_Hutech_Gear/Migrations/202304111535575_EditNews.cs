namespace Web_Hutech_Gear.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditNews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_News", "NewsCategory_Id", "dbo.tb_NewsCategory");
            DropIndex("dbo.tb_News", new[] { "NewsCategory_Id" });
            RenameColumn(table: "dbo.tb_News", name: "NewsCategory_Id", newName: "NewsCategoryId");
            AlterColumn("dbo.tb_News", "NewsCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_News", "NewsCategoryId");
            AddForeignKey("dbo.tb_News", "NewsCategoryId", "dbo.tb_NewsCategory", "Id", cascadeDelete: true);
            DropColumn("dbo.tb_News", "CategoryId");
        }

        public override void Down()
        {
            AddColumn("dbo.tb_News", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_News", "NewsCategoryId", "dbo.tb_NewsCategory");
            DropIndex("dbo.tb_News", new[] { "NewsCategoryId" });
            AlterColumn("dbo.tb_News", "NewsCategoryId", c => c.Int());
            RenameColumn(table: "dbo.tb_News", name: "NewsCategoryId", newName: "NewsCategory_Id");
            CreateIndex("dbo.tb_News", "NewsCategory_Id");
            AddForeignKey("dbo.tb_News", "NewsCategory_Id", "dbo.tb_NewsCategory", "Id");
        }
    }
}
