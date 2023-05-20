namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletepost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Posts", "NewsCategoryId", "dbo.tb_NewsCategory");
            DropIndex("dbo.tb_Posts", new[] { "NewsCategoryId" });
            DropTable("dbo.tb_Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tb_Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false, maxLength: 150),
                        Detail = c.String(nullable: false),
                        Image = c.String(nullable: false, maxLength: 250),
                        NewsCategoryId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.tb_Posts", "NewsCategoryId");
            AddForeignKey("dbo.tb_Posts", "NewsCategoryId", "dbo.tb_NewsCategory", "Id", cascadeDelete: true);
        }
    }
}
