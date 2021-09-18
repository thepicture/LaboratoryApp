﻿using LaboratoryApp.Models;
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
    }
}
