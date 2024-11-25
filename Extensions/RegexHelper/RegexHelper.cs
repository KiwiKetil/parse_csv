using System.Text.RegularExpressions;

namespace ParseCsv.RegexHelper;
public static partial class RegexHelper
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled)]
    private static partial Regex EmailRegex();

    public static bool IsValidEmail(string input)
    {
        return EmailRegex().IsMatch(input);
    }

    [GeneratedRegex(@"[^a-zA-Z0-9 ]")]
    private static partial Regex SpecialCharacterRegex();

    public static bool ContainsSpecialCharacters(string input)
    {
        return SpecialCharacterRegex().IsMatch(input);
    }
}
