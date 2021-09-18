using LaboratoryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppContext = LaboratoryApp.Models.AppContext;

namespace LaboratoryApp.Views
{
    /// <summary>
    /// Interaction logic for AssistantPage.xaml
    /// </summary>
    public partial class AssistantPage : Page
    {
        public AssistantPage()
        {
            InitializeComponent();

            ServiceOfOrderView.ItemsSource = AppContext.GetInstance().ServiceOfOrder.ToList();
        }

        private void ComboStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ComboBox comboStatus = sender as ComboBox;
            int orderId = (comboStatus.DataContext as ServiceOfOrder).OrderId;
            if (!SimpleMessager.ShowQuestion("Точно обработать статус заказа?"))
            {
                SimpleMessager.ShowMessage("Отмена статуса отменена!");
                return;
            }
            else
            {
                AppContext.GetInstance().ServiceOfOrder.Find(orderId).ServiceStatus = comboStatus.SelectedItem as ServiceStatus;
                try
                {
                    AppContext.GetInstance().SaveChanges();
                    SimpleMessager.ShowMessage("Статус успешно обновлён!");
                }
                catch (Exception ex)
                {
                    SimpleMessager.ShowError(ex.Message + ". Попробуйте поменять статус ещё раз.");
                }
            }
        }
    }
}
