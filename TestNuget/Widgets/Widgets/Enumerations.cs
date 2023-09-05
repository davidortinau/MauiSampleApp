namespace Widgets
{
    public class Enumerations
    {
        /// <summary>
        /// NIQDialog types
        /// </summary>
        public enum NIQDialogType
        {
            None,
            PositiveConfirmation,
            Error,
            Warning,
            Question,
            Information
        }

        /// <summary>
        /// Wrapper enum type for Entry keyboard type
        /// </summary>
        public enum NIQEntryKeyboardType
        {
            Default,
            Plain,
            Chat,
            Email,
            Numeric,
            Telephone,
            Text,
            Url
        }
    }
}
