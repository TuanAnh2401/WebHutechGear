namespace Web_Hutech_Gear.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class editAVT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Avatar");
        }
    }
}
