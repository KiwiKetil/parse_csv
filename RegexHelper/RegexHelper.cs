using System.Text.RegularExpressions;

namespace ParseCsv.RegexHelper;
public static partial class RegexHelper
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled)]
    public static partial Regex EmailRegex();

}
