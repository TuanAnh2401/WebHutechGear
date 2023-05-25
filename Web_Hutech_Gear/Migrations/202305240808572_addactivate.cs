namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addactivate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Adv", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_News", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_NewsCategory", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_ProductCategory", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_ProductImage", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Status", "IsActivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Supplier", "IsActivate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Supplier", "IsActivate");
            DropColumn("dbo.tb_Status", "IsActivate");
            DropColumn("dbo.tb_ProductImage", "IsActivate");
            DropColumn("dbo.tb_ProductCategory", "IsActivate");
            DropColumn("dbo.tb_Product", "IsActivate");
            DropColumn("dbo.tb_NewsCategory", "IsActivate");
            DropColumn("dbo.tb_News", "IsActivate");
            DropColumn("dbo.tb_Adv", "IsActivate");
        }
    }
}
