using System.Linq;

namespace Web_Hutech_Gear.Models.Support
{
    public class SettingHelper
    {
        public static string GetValue(string key)
        {
            using (var db = new ApplicationDbContext())
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
}