using System.Windows;

namespace LaboratoryApp.Models
{
    public class AppData
    {
        public static Window LoginWindow { get; set; }
        public static User CurrentUser { get; set; }
        public static string CurrentTitle { get; set; }
    }
}
