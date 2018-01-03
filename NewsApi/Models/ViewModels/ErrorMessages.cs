namespace NewsApi.Models.ViewModels
{
    /// <summary>
    /// A static collection of ModelState error messages
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Error message when Title is missing
        /// </summary>
        public const string TITLE_REQUIRED = "Title is required";

        /// <summary>
        /// Error message when Title is too long
        /// </summary>
        public const string TITLE_TOO_LONG = "Max length of title is 50";

        /// <summary>
        /// Error message when Content is missing
        /// </summary>
        public const string CONTENT_REQUIRED = "Content is required";
    }
}