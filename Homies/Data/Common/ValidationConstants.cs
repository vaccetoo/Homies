namespace Homies.Data.Common
{
    public static class ValidationConstants
    {
        // Event validation constants
        public const int EventNameMinLength = 5;
        public const int EventNameMaxLength = 20;

        public const int EventDescriptionMinLength = 15;
        public const int EventDescriptionMaxLength = 150;

        // Type validation constants
        public const int TypeNameMinLength = 5;
        public const int TypeNameMaxLength = 15;

        // DateTime format
        public const string DateFormat = "yyyy-MM-dd H:mm";

        // Error messages
        public const string RequiredErrorMessage = "The {0} field is required !";
        public const string LengthErrorMessage = "The {0} field must be between {2} and {1} symbols !";
        public const string InvalidDateFormat = $"Invalid date format ! Format must be : {DateFormat}";
    }
}
