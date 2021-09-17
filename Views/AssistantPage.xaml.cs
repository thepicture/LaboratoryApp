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

            ServiceOfOrderView.ItemsSource = Models.AppContext.GetInstance().ServiceOfOrder.ToList();
        }
    }
}
