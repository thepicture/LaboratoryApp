

using LaboratoryApp.Models;
using System.Linq;
using System.Windows;

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
        /// Exit the current app.
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

            User user = context.User.FirstOrDefault(u => u.Login.Equals(LoginBox) && u.Password.Equals(PBoxPassword));

            if (user == null)
            {
                bool isCaptchaReady = CaptchaUtils.NotifyAndCheckIfPrepared();

                if (isCaptchaReady)
                {
                    BitMapImage captcha = CaptchaUtils.GetCaptcha();
                }
            }
        }
    }
}
