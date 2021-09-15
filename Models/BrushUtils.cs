using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LaboratoryApp.Models
{
    public class BrushUtils
    {
        private static readonly IEnumerable<Brush> brushes = typeof(Brushes).GetProperties().Select(b => b.GetValue(null)).Where(b => !b.Equals(Brushes.Transparent)).Cast<Brush>();
        private static readonly Random random = new Random();

        /// <summary>
        /// Returns a random brush.
        /// </summary>
        public static Brush getRandomBrush()
        {
            return brushes.ElementAt(random.Next(0, brushes.Count()));
        }
    }
}
