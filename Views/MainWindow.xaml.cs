

using LaboratoryApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AppContext = LaboratoryApp.Models.AppContext;

namespace LaboratoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Exits the current app if the user wants to.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            bool isWantsToExit = SimpleMessager.ShowQuestion("Точно покинуть приложение?");

            if (isWantsToExit)
            {
                App.Current.Shutdown();
            }
        }

        /// <summary>
        /// Login for the user.
        /// </summary>
        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LaboratoryBaseEntities context = AppContext.GetInstance();

            User user = await Task.Run(() =>
            {
                return GetUserOrNull();
            });

            if (user == null)
            {
                SimpleMessager.ShowError("Неверный логин и/или пароль.\nПожалуйста, введите корректные данные.");

                CheckIfCaptchaIsReady();
            }
        }

        private User GetUserOrNull()
        {
            LaboratoryBaseEntities entities = new LaboratoryBaseEntities();
            User currentUser = null;
            Dispatcher.Invoke(() =>
            {
                string login = LoginBox.Text;
                string password = PBoxPassword.Password;

                currentUser = entities.User.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
            });
            return currentUser;
        }

        private void CheckIfCaptchaIsReady()
        {
            bool isCaptchaReady = CaptchaUtils.NotifyAndCheckIfPrepared();

            if (isCaptchaReady)
            {
                SetUserControlsEnabledState(false);
                InitCaptchaPanel();
            }
        }

        private void InitCaptchaPanel()
        {
            CaptchaPanel.Visibility = Visibility.Visible;

            InsertCaptcha();
        }

        private void HideCaptchaPanel()
        {
            CaptchaPanel.Visibility = Visibility.Collapsed;
        }

        private void InsertCaptcha()
        {
            CaptchaUtils.RegenerateCaptcha();

            DrawingImage captcha = CaptchaUtils.GetCaptcha();

            CaptchaImage.Source = captcha;
        }

        private void SetUserControlsEnabledState(bool isEnabled)
        {
            PBoxPassword.IsEnabled = isEnabled;
            LoginBox.IsEnabled = isEnabled;
            BtnLogin.IsEnabled = isEnabled;
        }

        private void ShowPasswordBox_Checked(object sender, RoutedEventArgs e)
        {
            TBoxPassword.Text = PBoxPassword.Password;
            TBoxPassword.Visibility = Visibility.Visible;
            PBoxPassword.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PBoxPassword.Password = TBoxPassword.Text;
            PBoxPassword.Visibility = Visibility.Visible;
            TBoxPassword.Visibility = Visibility.Collapsed;
        }

        private void BtnCheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaUtils.ValidateCaptcha(CaptchaBox.Text))
            {
                SetUserControlsEnabledState(true);
                HideCaptchaPanel();
            }
            else
            {
                BlockInterface();
            }
        }

        private void BlockInterface()
        {
            IsEnabled = false;

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10),
            };

            timer.Tick += TimerUnblockInterface;
            timer.Start();
        }

        private void TimerUnblockInterface(object sender, EventArgs e)
        {
            (sender as DispatcherTimer).Stop();
            IsEnabled = true;
        }

        private void BtnReloadCaptcha_Click(object sender, RoutedEventArgs e)
        {
            InsertCaptcha();
        }

        private void CaptchaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CaptchaDemonstrator.Visibility = Visibility.Visible;
        }

        private void BtnCloseGiantCaptcha_Click(object sender, RoutedEventArgs e)
        {
            CaptchaDemonstrator.Visibility = Visibility.Collapsed;
        }
    }
}
