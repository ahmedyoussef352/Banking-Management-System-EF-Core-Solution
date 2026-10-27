namespace Oop_CityLIbrarySystemProject.Helper;

public static class ConsoleHelper
{
    public static void ShowMenu()
    {
        ThemeHelper.PrintHeader("          National Bank - Management          ");
        ThemeHelper.PrintOption("1) Add a new Customer");
        ThemeHelper.PrintOption("2) Open a new Account for a Customer");
        ThemeHelper.PrintOption("3) Update Account Status (Active / Closed)");
        ThemeHelper.PrintOption("4) Remove an Account from a Customer");
        ThemeHelper.PrintOption("5) List all Customer");
        ThemeHelper.PrintOption("0) Exist");
        Console.Write("Enter your choice: ");
    }
}
