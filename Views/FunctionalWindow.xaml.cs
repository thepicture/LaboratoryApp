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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LaboratoryApp.Views
{
    /// <summary>
    /// Interaction logic for FunctionalWindow.xaml
    /// </summary>
    public partial class FunctionalWindow : Window
    {
        private TimeSpan accumulator = new TimeSpan(0, 0, 0);

        public FunctionalWindow()
        {
            InitializeComponent();

            AppData.MainFrame = MainFrame;

            string role = AppData.CurrentUser.TypeOfUser.Name;

            if (role == "Лаборант" || role == "Лаборант-исследователь")
            {
                InitializeTimerForTitle();
            }
        }

        private void InitializeTimerForTitle()
        {
            DispatcherTimer timer = SessionTimer.GetInstance();

            timer.Tick += Session_Tick;
            timer.Start();
        }

        private void Session_Tick(object sender, EventArgs e)
        {
            accumulator += TimeSpan.FromSeconds(1);

            if (accumulator.Minutes == 10 && accumulator.Seconds == 0)
            {
                Task.Run(() => SimpleMessager.ShowMessage("Сеанс окончится через 5 минут"));
            }
            else if (accumulator.Minutes == 5 && accumulator.Seconds == 0)
            {
                Task.Run(() => SimpleMessager.ShowMessage("Сеанс окончен"));

                CloseCurrentWindowAndBlockLogin();
                (sender as DispatcherTimer).Stop();
            }

            TitleUtils.AppendValue(GetHoursAndSeconds(), this);
        }

        private string GetHoursAndSeconds()
        {
            string hours = accumulator.Hours < 10 ? "0" + accumulator.Hours : accumulator.Hours.ToString();
            string minutes = accumulator.Minutes < 10 ? "0" + accumulator.Minutes : accumulator.Minutes.ToString();

            return hours + ":" + minutes;
        }

        private void CloseCurrentWindowAndBlockLogin()
        {
            Close();
            AppData.LoginWindow.Show();
        }

        private void FuncWin_ContentRendered(object sender, EventArgs e)
        {
            TitleUtils.AppendValue(GetHoursAndSeconds(), this);
        }
    }
}
