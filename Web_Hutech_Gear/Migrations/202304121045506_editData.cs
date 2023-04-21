namespace Web_Hutech_Gear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editData : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_SystemSetting", "SettingDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_SystemSetting", "SettingDescription", c => c.String(maxLength: 4000));
        }
    }
}
