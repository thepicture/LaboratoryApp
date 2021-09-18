namespace LaboratoryApp.Models
{
    /// <summary>
    /// The interface in which user can create reports of entities based on implementation.
    /// </summary>
    public interface IReportFormer
    {
        /// <summary>
        /// Forms the report in .doc and .pdf formats in the destination path.
        /// Shows the report after forming if isVisible is true,
        /// otherwise false.
        /// </summary>
        /// <param name="destinationPath">Where the report will be saved.</param>
        /// <param name="isVisible">If the report visible after forming.</param>
        void Form(string destinationPath, bool isVisible);
    }
}
