using System.Linq;

namespace Web_Hutech_Gear.Models.Support
{
    public class SettingHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static string GetValue(string key)
        {
            var item = db.SystemSettings.SingleOrDefault(x => x.SettingKey == key);
            if (item != null)
            {
                return item.SettingValue;
            }
            return "";
        }
    }
}