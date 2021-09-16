using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1),
                };

            }
            return timer;
        }
    }
}
