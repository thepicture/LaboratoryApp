namespace LaboratoryApp.Models
{
    /// <summary>
    /// The type which representing a checking of the IsArchive attribute.
    /// </summary>
    public class ArchivedChecker
    {
        /// <summary>
        /// Checks if the item is archived in the database.
        /// It signals that the item was deleted if returns true,
        /// otherwise item was not deleted or was recovered.
        /// </summary>
        /// <param name="value">IsArchived attribute value from database.</param>
        /// <returns>Boolean which representing if the item is archived.</returns>
        public static bool IsArchived(bool? value)
        {
            return value != null && value != false;
        }
    }
}
