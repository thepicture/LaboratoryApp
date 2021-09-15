

using LaboratoryApp.Models;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

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
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LaboratoryBaseEntities context = AppContext.GetInstance();

            User user = context.User.FirstOrDefault(u => u.Login.Equals(LoginBox.Text) && u.Password.Equals(PBoxPassword.Password));

            if (user == null)
            {
                SimpleMessager.ShowError("Неверный логин и/или пароль.\nПожалуйста, введите корректные данные.");

                checkIfCaptchaIsReady();
            }
        }

        private void checkIfCaptchaIsReady()
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
            BitmapImage captcha = CaptchaUtils.GetCaptcha();

            CaptchaImage.Source = captcha;
        }

        private void SetUserControlsEnabledState(bool isEnabled)
        {
            PBoxPassword.IsEnabled = isEnabled;
            LoginBox.IsEnabled = isEnabled;
            BtnLogin.IsEnabled = IsEnabled;
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
    }
}
