namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Image = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Product", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Product", "SupplierId");
            AddForeignKey("dbo.tb_Product", "SupplierId", "dbo.tb_Supplier", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Product", "SupplierId", "dbo.tb_Supplier");
            DropIndex("dbo.tb_Product", new[] { "SupplierId" });
            DropColumn("dbo.tb_Product", "SupplierId");
            DropTable("dbo.tb_Supplier");
        }
    }
}
