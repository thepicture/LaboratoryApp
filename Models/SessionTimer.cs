using System;
using System.Windows.Threading;

namespace LaboratoryApp.Models
{
    public class SessionTimer
    {
        private static DispatcherTimer timer;
        public static DispatcherTimer GetInstance()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer(DispatcherPriority.Background)
                {
                    Interval = TimeSpan.FromSeconds(1),
                };

            }
            return timer;
        }
    }
}
