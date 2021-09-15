using System;
using System.Windows.Media.Imaging;

namespace LaboratoryApp.Models
{
    public class CaptchaUtils
    {
        private static readonly int MAX_ATTEMPT_COUNT = 2;
        private static int attempts = 0;
        private static string _captcha;
        /// <summary>
        /// Notifies captcha that login attempt is incorrect and this class must return his state about captcha image.
        /// </summary>
        /// <returns></returns>
        public static bool NotifyAndCheckIfPrepared()
        {
            if (MAX_ATTEMPT_COUNT == attempts)
            {
                PrepareCaptcha();
                attempts = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prepares the captcha.
        /// </summary>
        private static void PrepareCaptcha()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the captcha.
        /// </summary>
        private static BitmapImage GetCaptcha()
        {
            return null;
        }

        private static bool ValidateCaptcha(string captcha)
        {
            if (captcha != _captcha)
            {
                return false;
            }
            else
            {
                Reset();
                return true;
            }
        }

        private static void Reset()
        {
            _captcha = null;
        }
    }
}
