using LaboratoryApp.Models;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Controls;
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

        /// <summary>
        /// Changes the current status of the material with feedback from the system.
        /// </summary>
        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboStatus = sender as ComboBox;
            int orderId = (comboStatus.DataContext as ServiceOfOrder).OrderId;
            int serviceId = (comboStatus.DataContext as ServiceOfOrder).ServiceId;

            AppContext.GetInstance().ServiceOfOrder.Find(new object[] { serviceId, orderId }).ServiceStatus = comboStatus.SelectedItem as ServiceStatus;
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

        /// <summary>
        /// Forms a new report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if ((bool)dialog.ShowDialog())
            {
                new ServiceOfOrderReportFormer().Form(dialog.FileName, true);
                SimpleMessager.ShowMessage("Документы успешно сохранены!");
            }
            else
            {
                SimpleMessager.ShowMessage("Формирование отчёта отменено!");
            }
        }
    }
}
