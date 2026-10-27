using BankEfCore;
using Oop_CityLIbrarySystemProject.Helper;
using Oop_CityLIbrarySystemProject.Serviecs;

//Console.WriteLine("Hello, World!");

var dbContext = new ApplicationDbContext();
var serviec = new BankService(dbContext);
/* 1-Seed
//if (!dbContext.Branchs.Any())
//{
//    using var transaction = await dbContext.Database.BeginTransactionAsync();
//    try
//    {
//        List<Manager> managers = [
//            new Manager { FullName = "Ahmed Mohamed", Email = "ahmed.manager@bank.com", PhoneNumber = "01012345678", HireDate = new DateTime(2020, 1, 15) },
//            new Manager { FullName = "Sara Ali", Email = "sara.manager@bank.com", PhoneNumber = "01098765432", HireDate = new DateTime(2021, 5, 10) },
//            new Manager { FullName = "Khaled Youssef", Email = "khaled.manager@bank.com", PhoneNumber = "01055544332", HireDate = new DateTime(2019, 3, 20) },
//            new Manager { FullName = "Mona Adel", Email = "mona.manager@bank.com", PhoneNumber = "01077788991", HireDate = new DateTime(2022, 7, 11) },
//            new Manager { FullName = "Tamer Hosny", Email = "tamer.manager@bank.com", PhoneNumber = "01033322114", HireDate = new DateTime(2023, 2, 5) }
//        ];
//        dbContext.Managers.AddRange(managers);
//        await dbContext.SaveChangesAsync();

//        List<Customer> customers = [
//            new Customer { FullName = "Mahmoud Hassan", Email = "mahmoud@gmail.com", PhoneNumber = "01122334455", Address = "Nasr City, Cairo", DateOfBirth = new DateTime(1995, 6, 20), NationalId = 29506200101234, CustomerType = CustomerType.VIP },
//            new Customer { FullName = "Tech Solutions LLC", Email = "info@techsolutions.com", PhoneNumber = "01233445566", Address = "Smart Village, Giza", DateOfBirth = new DateTime(2015, 3, 10), NationalId = 31503100201234, CustomerType = CustomerType.Corporate },
//            new Customer { FullName = "Fatma Omar", Email = "fatma@gmail.com", PhoneNumber = "01199887766", Address = "Smouha, Alexandria", DateOfBirth = new DateTime(1990, 11, 5), NationalId = 29011050109876, CustomerType = CustomerType.Normal },
//            new Customer { FullName = "Global Trading Co.", Email = "contact@global.com", PhoneNumber = "01255443322", Address = "Maadi, Cairo", DateOfBirth = new DateTime(2018, 5, 12), NationalId = 31805120205544, CustomerType = CustomerType.Corporate },
//            new Customer { FullName = "Youssef Nader", Email = "youssef@gmail.com", PhoneNumber = "01144332211", Address = "Tanta, Gharbia", DateOfBirth = new DateTime(2000, 1, 15), NationalId = 30001150101122, CustomerType = CustomerType.VIP }
//        ];
//        dbContext.Customers.AddRange(customers);
//        await dbContext.SaveChangesAsync();

//        List<Branch> branches = [
//            new Branch { Name = "Cairo Main Branch", Address = "Tahrir Square, Cairo", PhoneNumber = "0223445566", ManagerId = managers[0].Id },
//            new Branch { Name = "Alexandria Branch", Address = "Corniche, Alexandria", PhoneNumber = "035443322", ManagerId = managers[1].Id }
//        ];
//        dbContext.Branchs.AddRange(branches);
//        await dbContext.SaveChangesAsync();

//        List<Account> accounts = [
//            new Account { CurrentBalance = 50000.00m, AccountType = AccountType.Current, OpeningDate = new DateTime(2023, 1, 1), BranchCode = branches[0].Code },
//            new Account { CurrentBalance = 120500.50m, AccountType = AccountType.Savings, OpeningDate = new DateTime(2023, 6, 15), BranchCode = branches[1].Code },
//            new Account { CurrentBalance = 35000.00m, AccountType = AccountType.Savings, OpeningDate = new DateTime(2023, 3, 10), BranchCode = branches[0].Code },
//            new Account { CurrentBalance = 250000.75m, AccountType = AccountType.Current, OpeningDate = new DateTime(2023, 4, 22), BranchCode = branches[1].Code },
//            new Account { CurrentBalance = 15000.00m, AccountType = AccountType.Savings, OpeningDate = new DateTime(2023, 5, 5), BranchCode = branches[0].Code }
//        ];
//        dbContext.Accounts.AddRange(accounts);
//        await dbContext.SaveChangesAsync();

//        List<CustomerAccount> customerAccounts = [
//            new CustomerAccount { CustomerId = customers[0].Id, AccountNumber = accounts[0].AccountNumber, OwnershipStartDate = new DateTime(2023, 1, 1), OwnershipType = "Primary", AccountStatus = "Active" },
//            new CustomerAccount { CustomerId = customers[0].Id, AccountNumber = accounts[1].AccountNumber, OwnershipStartDate = new DateTime(2023, 6, 15), OwnershipType = "Primary", AccountStatus = "Active" },
//            new CustomerAccount { CustomerId = customers[1].Id, AccountNumber = accounts[1].AccountNumber, OwnershipStartDate = new DateTime(2023, 6, 15), OwnershipType = "Co-Holder", AccountStatus = "Active" },
//            new CustomerAccount { CustomerId = customers[2].Id, AccountNumber = accounts[2].AccountNumber, OwnershipStartDate = new DateTime(2023, 3, 10), OwnershipType = "Primary", AccountStatus = "Active" },
//            new CustomerAccount { CustomerId = customers[3].Id, AccountNumber = accounts[3].AccountNumber, OwnershipStartDate = new DateTime(2023, 4, 22), OwnershipType = "Primary", AccountStatus = "Active" },
//            new CustomerAccount { CustomerId = customers[4].Id, AccountNumber = accounts[4].AccountNumber, OwnershipStartDate = new DateTime(2023, 5, 5), OwnershipType = "Primary", AccountStatus = "Active" }
//        ];
//        dbContext.CustomerAccounts.AddRange(customerAccounts);
//        await dbContext.SaveChangesAsync();

//        List<Transaction> transactions = [
//            new Transaction { TransactionDate = new DateTime(2023, 1, 2), Amount = 10000.00m, TransactionType = TransactionType.Deposit, Note = "Initial Deposit", AccountNumber = accounts[0].AccountNumber },
//            new Transaction { TransactionDate = new DateTime(2023, 1, 5), Amount = 2000.00m, TransactionType = TransactionType.Withdrawal, Note = "ATM Withdrawal", AccountNumber = accounts[0].AccountNumber },
//            new Transaction { TransactionDate = new DateTime(2023, 3, 15), Amount = 15000.00m, TransactionType = TransactionType.Deposit, Note = "Salary Deposit", AccountNumber = accounts[2].AccountNumber },
//            new Transaction { TransactionDate = new DateTime(2023, 4, 25), Amount = 50000.00m, TransactionType = TransactionType.Deposit, Note = "Corporate Capital", AccountNumber = accounts[3].AccountNumber },
//            new Transaction { TransactionDate = new DateTime(2023, 5, 10), Amount = 3000.00m, TransactionType = TransactionType.Withdrawal, Note = "Online Purchase", AccountNumber = accounts[4].AccountNumber }
//        ];
//        dbContext.Transactions.AddRange(transactions);
//        await dbContext.SaveChangesAsync();

//        await transaction.CommitAsync();
//    }
//    catch (Exception)
//    {
//        await transaction.RollbackAsync();
//        throw;
//    }
//}*/
bool running = true;

while (running)
{
    try
    {
        ConsoleHelper.ShowMenu();

        string? choice = Console.ReadLine()?.Trim();
        Console.WriteLine();

        switch (choice)
        {
            case "1":

                await serviec.AddCustomerHandel();
                break;

            case "2":
                await serviec.OpenNewAccount();
                break;

            case "3":
                await serviec.UpdateAccountStatus();
                break;

            case "4":
                await serviec.RemoveByCustomerIdAndAccountNumber();
                break;

            case "5":
                await serviec.PrintListOfCustomersAndAccount();
                break;

            case "0":
                Console.WriteLine("* Goodbye!*");
                running = false;
                break;

            default:
                Console.WriteLine("* Invalid option. Try again.*");
                break;
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }

    Console.WriteLine("\n Press Enter to continue...");
    Console.ReadLine();
}

