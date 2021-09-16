using LaboratoryApp.Views;
using System.Windows.Controls;

namespace LaboratoryApp.Models
{
    public class UserPageFactory
    {
        public static Page CreatePageFor(User u)
        {
            switch (u.TypeOfUser.Name)
            {
                case "Лаборант":
                    return new AssistantPage();
                case "Лаборант-исследователь":
                    return new AssistantInventorPage();
                case "Бухгалтер":
                    return new AccountantPage();
                case "Администратор":
                    return new AdminPage();
                default:
                    return null;
            }
        }
    }
}
