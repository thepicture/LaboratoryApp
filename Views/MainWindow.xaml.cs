

using LaboratoryApp.Models;
using LaboratoryApp.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            LoadingRect.Visibility = Visibility.Visible;

            User user = await Task.Run(() =>
            {
                LaboratoryBaseEntities entities = AppContext.GetInstance();
                User currentUser = null;
                string[] credentials = GetControlsCredentials();
                string login = credentials[0];
                string password = credentials[1];

                currentUser = entities.User.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));

                return currentUser;
            });

            LoadingRect.Visibility = Visibility.Hidden;

            if (user == null)
            {
                SimpleMessager.ShowError("Неверный логин и/или пароль.\nПожалуйста, введите корректные данные.");

                CheckIfCaptchaIsReady();
            }
            else
            {
                TryToLogin(user);
            }
        }

        private void TryToLogin(User user)
        {
            if (user != null)
            {
                OpenFunctionalWindow(user);
            }
            else
            {
                SimpleMessager.ShowError($"Не удалось загрузить окно интерфейса для роли" +
                    $" {user.TypeOfUser.Name}." +
                    $"\nПожалуйста, обратитесь к администратору.");
            }
        }

        private void OpenFunctionalWindow(User user)
        {
            AppData.CurrentUser = user;

            SimpleMessager.ShowMessage("Добро пожаловать, " + user.FullName + "!");
            FunctionalWindow funcWindow = new FunctionalWindow();

            AppData.LoginWindow = this;
            AppData.CurrentTitle = user.FullName + " - " + user.TypeOfUser.Name;

            funcWindow.Show();
            AppData.MainFrame.Navigate(UserPageFactory.CreatePageFor(user));

            Hide();
        }

        private string[] GetControlsCredentials()
        {
            string[] credentials = null;
            Dispatcher.Invoke(() =>
            {
                credentials = new string[] { LoginBox.Text, PBoxPassword.Password };
            });
            return credentials;
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
