namespace ParseCsv.ParseV1;

using RegexHelper;

public static class ValidateFieldsV1
{
    public static HashSet<string> ValidateCsvFields(string firstName, string lastName, string email, string country)
    {
        HashSet<string> validationErrors = [];

        if (RegexHelper.ContainsSpecialCharacters(firstName))
        {
            validationErrors.Add($"Invalid firstName: {firstName}");
        }

        if (RegexHelper.ContainsSpecialCharacters(lastName))
        {
            validationErrors.Add($"Invalid lastName: {lastName}");
        }

        if (!RegexHelper.IsValidEmail(email))
        {
            validationErrors.Add($"Invalid email: {email}");
        }

        if (RegexHelper.ContainsSpecialCharacters(country))
        {
            validationErrors.Add($"Invalid country: {country}");
        }
        return validationErrors;
    }
}
