//namespace ParseCsv.ParseV2;
//public static class ValidateFieldsV2
//{
//    public static HashSet<string> ValidateCsvFields(string name, string hex, string rgb)
//    {
//        HashSet<string> validationErrors = [];

//        if (string.IsNullOrWhiteSpace(name))
//        {
//            validationErrors.Add($"Empty or whitespace field: {nameof(name)}");
//        }

//        if (string.IsNullOrWhiteSpace(hex))
//        {
//            validationErrors.Add($"Empty or whitespace field: {nameof(hex)}");
//        }

//        if (string.IsNullOrWhiteSpace(rgb))
//        {
//            validationErrors.Add($"Empty or whitespace field: {nameof(rgb)}");
//        }

//        if (RegexHelper.ContainsSpecialCharacters(name))
//        {
//            validationErrors.Add($"Invalid Name: {name}");
//        }

//        return validationErrors;
//    }
//}
