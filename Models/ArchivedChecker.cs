using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryApp.Models
{
    public class ArchivedChecker
    {
        /// <summary>
        /// Checks if the item is archived in the database.
        /// It signals that the item was deleted if returns true,
        /// otherwise item was not deleted or was recovered.
        /// </summary>
        /// <param name="value">IsArchived attribute value from database.</param>
        /// <returns>Boolean which representing if the item is archived</returns>
        public static bool IsArchived(bool? value)
        {
            if (value == null || value == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
