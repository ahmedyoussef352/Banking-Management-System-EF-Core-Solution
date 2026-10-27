namespace BankEfCore.Extensions;

public static class StringExtensions
{
    // Date
    public static bool IsValidDate(this string input, out DateTime parsedDate)
    {
        return DateTime.TryParse(input, out parsedDate);
    }

    // int 
    public static bool IsValidInt(this string input, out int result)
    {
        return int.TryParse(input, out result);
    }
    // Id
    public static string NormalizedId(this string id)
        => id?.Trim().ToUpperInvariant()!;

    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrEmpty(email)) return false;
        bool hasAt = false;
        bool hasDot = false;
        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == '@') hasAt = true;
            if (email[i] == '.') hasDot = true;


        }
        return hasAt && hasDot;
    }

    public static bool ContainDigit(this string id)
    {
        if (string.IsNullOrEmpty(id))
            return false;
        for (int i = 0; i < id.Length; i++)
        {
            if (char.IsDigit(id[i]))
                return true;
        }
        return false;
    }

    public static bool IsValidNationalID(this string input, int expectedLength = 14)
    {
        return !string.IsNullOrEmpty(input) &&
               input.Length == expectedLength &&
               input.All(char.IsDigit);
    }

}
