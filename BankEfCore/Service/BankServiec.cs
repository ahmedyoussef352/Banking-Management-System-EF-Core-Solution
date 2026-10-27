
using BankEfCore;
using BankEfCore.Entities;
using BankEfCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Oop_CityLIbrarySystemProject.Helper;

namespace Oop_CityLIbrarySystemProject.Serviecs;

public class BankService(ApplicationDbContext dbContext)
{
    public async Task AddCustomerHandel()
    {
        // 1
        string fullName =
            ThemeHelper.PromptAndValidate(
                "Full Name     : ",
                val => !string.IsNullOrWhiteSpace(val),
                "Full name cannot be empty.");
        //2
        string nationalIdInput =
            ThemeHelper.PromptAndValidate(
                "National ID   : ",
                val => val.IsValidNationalID(14),
                "Invalid National ID. Must be exactly 14 digits and contain numbers only."
                );
        //3
        DateTime finalDob = ThemeHelper.PromptAndParse<DateTime>("DateOfBirth (YYYY-MM-DD)", input =>
        {
            bool isValid = input.IsValidDate(out DateTime date);
            return (isValid, date);
        }, "Invalid date format. Please enter a valid date (e.g., 1995-05-12).");
        //4
        string email = ThemeHelper.PromptAndValidate("Email         : ",
            val => val.IsValidEmail(),
            "Invalid email format. Must contain '@' and domain.");
        //5
        string Phone = ThemeHelper.PromptAndValidate("Phone         : ",
            val => !string.IsNullOrWhiteSpace(val) && val.All(char.IsDigit),
            "Phone number must contain at least one digit."
        );
        //6
        string Address = ThemeHelper.PromptAndValidate("Address       : ",
            val => !string.IsNullOrWhiteSpace(val),
            "Address cannot be empty.");
        // 7. Customer Type
        CustomerType customerType =
            ThemeHelper.PromptAndValidateChoice<CustomerType>("Customer Type:\n    1) Normal\n    2) VIP\n    3) Corporate", choice => choice switch
        {
            "1" => (true, CustomerType.Normal),
            "2" => (true, CustomerType.VIP),
            "3" => (true, CustomerType.Corporate),
            _ => (false, default)
        }, "Choose 1, 2, or 3");
        var customer = new Customer
        {
            FullName = fullName,
            NationalId = long.Parse(nationalIdInput),
            Address = Address,
            DateOfBirth = finalDob,
            Email = email,
            PhoneNumber = Phone,
            CustomerType = customerType
        };
        dbContext.Customers.Add(customer);
        dbContext.Database.SetCommandTimeout(60);
        await dbContext.SaveChangesAsync();
        // Success Message

        ThemeHelper.PrintSuccess($"\nCustomer created successfully. CustomerId = {customer.Id}");


    }
    public async Task OpenNewAccount()
    {

        //3
        int branchCode = ThemeHelper.PromptAndParse<int>("BranchCode       : ",
            input =>
            {
                bool success = int.TryParse(input, out int val);
                return (success, val);

            }, "Invalid Branch Code. Must be a valid integer.");
        //4
        int customerId = ThemeHelper.PromptAndParse<int>("Customer ID       : ",
            input =>
            {
                bool success = int.TryParse(input, out int val);
                return (success, val);

            }, "Invalid Customer ID. Must be a valid integer.");
        //2 Account Type
        AccountType accoutType =
           ThemeHelper.PromptAndValidateChoice<AccountType>("Account Type:\n    1) Current\n    2) Savings\n", choice => choice switch
           {
               "1" => (true, AccountType.Current),
               "2" => (true, AccountType.Savings),
               _ => (false, default)
           }, "Choose 1 or 2");
        //5
        OwnershipType ownershipType =
           ThemeHelper.PromptAndValidateChoice<OwnershipType>("wnership Type:\n    1) Primary\n    2) CoHolder\n", choice => choice switch
           {
               "1" => (true, OwnershipType.Primary),
               "2" => (true, OwnershipType.CoHolder),
               _ => (false, default)
           }, "Choose 1 or 2");

        var account = new Account
        {
            CurrentBalance = 0.00m,
            BranchCode = branchCode,
            AccountType = accoutType,
            OpeningDate = DateTime.Now,
        };
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();



        var customerAccount = new CustomerAccount
        {
            CustomerId = customerId,
            AccountNumber = account.AccountNumber,
            OwnershipType = ownershipType,
            OwnershipStartDate = DateTime.Now,
            AccountStatus = "Active",

        };
        dbContext.CustomerAccounts.Add(customerAccount);
        dbContext.Database.SetCommandTimeout(60);
        await dbContext.SaveChangesAsync();
        // Success Message

        ThemeHelper.PrintSuccess($"\nAccount '{account.AccountNumber}' created and linked to customer {customerId} as primary owner");

    }
    public async Task UpdateAccountStatus()
    {
        // Account Number 
        int accountNumber = ThemeHelper.PromptAndParse<int>("account Number       : ",
            input =>
            {
                bool success = int.TryParse(input, out int val);
                return (success, val);

            }, "Invalid Account Number. Must be a valid integer.");
        // Customer Id 
        int customerId = ThemeHelper.PromptAndParse<int>("Customer ID       : ",
            input =>
            {
                bool success = int.TryParse(input, out int val);
                return (success, val);

            }, "Invalid Customer ID. Must be a valid integer.");
        // New Status
        string accountStatus = ThemeHelper.PromptAndValidateChoice<string>(
        "Ownership Type:\n    1) Active\n    2) Closed\n", choice =>
        {
            (bool, string?) value = choice switch
            {
                "1" => (true, "Active"),
                "2" => (true, "Closed"),
                _ => (false, default)
            };
            return value;
        }, "Choose 1 or 2");

        await dbContext.CustomerAccounts
                .Where(ca => ca.CustomerId == customerId && ca.AccountNumber == accountNumber)
                .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.AccountStatus, accountStatus));
        await dbContext.SaveChangesAsync();
        ThemeHelper.PrintSuccess($"\nStatus updated To {accountStatus}");

    }
    public async Task RemoveByCustomerIdAndAccountNumber()
    {
        // Account Number 
        int accountNumber = ThemeHelper.PromptAndParse<int>("account Number       : ",
            input =>
            {
                bool success = int.TryParse(input, out int val);
                return (success, val);

            }, "Invalid Account Number. Must be a valid integer.");
        // Customer Id 
        int customerId = ThemeHelper.PromptAndParse<int>("Customer ID       : ",
            input =>
            {
                bool success = int.TryParse(input, out int val);
                return (success, val);

            }, "Invalid Customer ID. Must be a valid integer.");


        var affectedRows = await dbContext.CustomerAccounts
                .Where(ca => ca.CustomerId == customerId && ca.AccountNumber == accountNumber)
                .ExecuteDeleteAsync();


        if (affectedRows > 0)
        {
            ThemeHelper.PrintSuccess("\nAccount removed successfully.");
        }
        else
        {
            ThemeHelper.PrintError("\nAccount not found or already deleted.");
        }

    }

    public async Task PrintListOfCustomersAndAccount()
    {
        var customers = await dbContext.Customers
            .Include(c => c.CustomerAccounts)
            .ThenInclude(ca => ca.Account)
            .AsNoTracking()
            .ToListAsync();
        if (!customers.Any())
        {
            Console.WriteLine("\nNo customers found.");
            return;
        }


        Console.WriteLine("     --- All Customers ---       \n ");

        foreach (var customer in customers)
        {
            ThemeHelper.PrintSuccess($"#{customer.Id} {customer.FullName} ({customer.CustomerType})");

            if (!customer.CustomerAccounts.Any())
            {
                ThemeHelper.PrintError("    (no accounts)");
                continue;
            }

            foreach (var customerAccount in customer.CustomerAccounts)
            {
                var account = customerAccount.Account;

                var accountNumber = customerAccount.AccountNumber;
                var accountType = account?.AccountType ?? AccountType.Current;
                var balance = account?.CurrentBalance ?? 0;
                var ownershipType = customerAccount.OwnershipType;
                var accountStatus = customerAccount.AccountStatus;
                var branchName = account?.Branch?.Name ?? "Main Branch";

                ThemeHelper.PrintSuccess($"    {accountNumber}    {accountType}    Balance:    {balance:N2}    {ownershipType}    {accountStatus}    @ {branchName}");
            }
        }
    }
}
