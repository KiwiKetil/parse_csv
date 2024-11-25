//namespace ParseCsv.ParseYield;
//public static class ValidateFieldsV3
//{
//    public static HashSet<string> ValidateCsvFields(string province, string abbreviation)
//    {
//        HashSet<string> validationErrors = [];

//        if (string.IsNullOrWhiteSpace(province))
//        {
//            validationErrors.Add($"Empty or whitespace field: {nameof(province)}");
//        }

//        if (string.IsNullOrWhiteSpace(abbreviation))
//        {
//            validationErrors.Add($"Empty or whitespace field: {nameof(abbreviation)}");
//        }

//        if (RegexHelper.ContainsSpecialCharacters(province))
//        {
//            validationErrors.Add($"Invalid Name: {province}");
//        }

//        if (RegexHelper.ContainsSpecialCharacters(abbreviation))
//        {
//            validationErrors.Add($"Invalid Name: {abbreviation}");
//        }

//        return validationErrors;
//    }
//}
