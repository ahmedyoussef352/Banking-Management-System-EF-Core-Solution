namespace Oop_CityLIbrarySystemProject.Helper
;

public static class ThemeHelper
{
    public static void PrintHeader(string title)
    {
        int num = 40;
        Console.WriteLine(new string('=', num));
        Console.WriteLine(title.PadLeft((num + title.Length) / 2));
        Console.WriteLine(new string('=', num));
    }

    public static void PrintSectionTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        int num = 40;
        int count = (num - title.Length - 2) / 2;
        string value = new string('-', count);
        Console.WriteLine($"{value} {title} {value}");
        Console.ResetColor();
    }

    public static void PrintOption(string option)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("  " + option);
        Console.ResetColor();
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message ?? "");
        Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message ?? "");
        Console.ResetColor();
    }

    public static void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(message ?? "");
        Console.ResetColor();
    }

    public static string Prompt(string label)
    {
        string text;
        do
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nEnter " + label + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            text = Console.ReadLine()?.Trim();
            Console.ResetColor();
            if (string.IsNullOrWhiteSpace(text))
            {
                PrintWarning(label + " cannot be empty. Please try again.");
            }
        }
        while (string.IsNullOrWhiteSpace(text));
        return text;
    }

    public static string PromptAndValidate
        (string prompt, Func<string, bool> validate, string errorMessage)
    {
        while (true)
        {
            try
            {
                string input = Prompt(prompt);
                if (!validate(input))
                {
                    throw new InvalidOperationException(errorMessage);
                }
                return input;
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

    }
    public static T PromptAndParse<T>(string prompt, Func<string, (bool IsValid, T ParsedValue)> parser, string errorMessage)
    {
        while (true)
        {
            try
            {
                string input = Prompt(prompt);
                var result = parser(input);

                if (!result.IsValid)
                {
                    throw new InvalidOperationException(errorMessage);
                }

                return result.ParsedValue;
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }
    }
    public static TEnum PromptAndValidateChoice<TEnum>(string promptMenu, Func<string, (bool IsValid, TEnum Result)> choiceEvaluator, string errorMessage)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(promptMenu);
                string choice = Prompt("");
                var evaluation = choiceEvaluator(choice);

                if (!evaluation.IsValid)
                {
                    throw new InvalidOperationException(errorMessage);
                }

                return evaluation.Result;
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }
    }
}