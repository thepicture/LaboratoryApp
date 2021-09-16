using System.Windows;

namespace LaboratoryApp.Models
{
    public class TitleUtils
    {
        public static void AppendValue(string value, Window window)
        {
            if (AppData.CurrentTitle == null)
            {
                return;
            }

            window.Title = AppData.CurrentTitle + " " + value;
        }
    }
}
