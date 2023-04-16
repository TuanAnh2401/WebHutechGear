namespace Web_Hutech_Gear.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Posts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 150),
                    Description = c.String(maxLength: 150),
                    Detail = c.String(),
                    Image = c.String(maxLength: 250),
                    NewsCategoryId = c.Int(nullable: false),
                    CreatedBy = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    Modifiedby = c.String(),
                    ModifiedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_NewsCategory", t => t.NewsCategoryId, cascadeDelete: true)
                .Index(t => t.NewsCategoryId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.tb_Posts", "NewsCategoryId", "dbo.tb_NewsCategory");
            DropIndex("dbo.tb_Posts", new[] { "NewsCategoryId" });
            DropTable("dbo.tb_Posts");
        }
    }
}
