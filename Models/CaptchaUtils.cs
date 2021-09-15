using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LaboratoryApp.Models
{
    public class CaptchaUtils
    {
        private static readonly int MAX_ATTEMPT_COUNT = 2;
        private static readonly int FONT_SIZE = 20;
        private static readonly int SYMBOLS_COUNT = 4;
        private static readonly int WIDTH = 200;
        private static readonly int HEIGHT = 200;
        private static int attempts = 0;
        private static string _captcha;
        private static DrawingImage currentCaptcha;
        private static Random random;

        /// <summary>
        /// Notifies the captcha that a login attempt is incorrect and this class must return its state about captcha image.
        /// </summary>
        /// <returns></returns>
        public static bool NotifyAndCheckIfPrepared()
        {
            attempts++;

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
            _captcha = GenerateCaptchaText();

            var visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();

            context.DrawRectangle(BrushUtils.GetRandomBrush(), null, new Rect(0, 0, WIDTH, HEIGHT));
            context.DrawLine(new Pen(BrushUtils.GetRandomBrush(), random.Next(0, HEIGHT / 16)), new Point(0, random.Next(0, HEIGHT)), new Point(WIDTH, random.Next(0, HEIGHT)));
            context.DrawLine(new Pen(BrushUtils.GetRandomBrush(), random.Next(0, HEIGHT / 16)), new Point(0, random.Next(0, HEIGHT)), new Point(WIDTH, random.Next(0, HEIGHT)));

            for (int i = 0; i < SYMBOLS_COUNT; i++)
            {
                FormattedText text = new FormattedText(_captcha, System.Globalization.CultureInfo.InvariantCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Times New Roman"), FONT_SIZE, BrushUtils.GetRandomBrush(), VisualTreeHelper.GetDpi(visual).PixelsPerDip);
                context.DrawText(text, new System.Windows.Point(WIDTH / SYMBOLS_COUNT * i + random.Next(-WIDTH, WIDTH), HEIGHT / SYMBOLS_COUNT * i + random.Next(-HEIGHT, HEIGHT)));
            }
            context.Close();

            currentCaptcha = new DrawingImage(visual.Drawing);
        }

        private static string GenerateCaptchaText()
        {
            if (random == null)
            {
                random = new Random();
            }

            string symbols = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < SYMBOLS_COUNT; i++)
            {
                result.Append(symbols[random.Next(0, symbols.Length)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets the captcha.
        /// </summary>
        public static DrawingImage GetCaptcha()
        {
            return currentCaptcha;
        }

        /// <summary>
        /// Checks if the captcha is correct.
        /// </summary>
        /// <param name="captcha"></param>
        /// <returns></returns>
        public static bool ValidateCaptcha(string captcha)
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
