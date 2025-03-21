using System.Threading.Channels;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Facade;
using FinanceTrackerApp.Domain.Patterns.Facade.AnalyticFacade;
using FinanceTrackerApp.Domain.Patterns.Facade.DataTransferFacade;

namespace FinanceTrackerApp;

public class UI
{
    private readonly IBankAccountFacade _bankAccountFacade;
    private readonly ICategoryFacade _categoryFacade;
    private readonly IOperationFacade _operationFacade;
    private readonly IAnalyticFacade _analyticFacade;
    private readonly IDataTransferFacade _transferFacade;

    public UI(
        IBankAccountFacade bankAccountFacade,
        ICategoryFacade categoryFacade,
        IOperationFacade operationFacade,
        IAnalyticFacade analyticFacade,
        IDataTransferFacade transferFacade
    )
    {
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
        _analyticFacade = analyticFacade;
        _transferFacade = transferFacade;
    }
    
    public void RunApp()
    {
        while (true)
        {
            PrintMenu();
            var c = Console.ReadLine();
            CheckInput(ref c);
            switch (c)
            {
                case "1":
                    PrintAccountMenu();
                    c = Console.ReadLine();
                    CheckInput(ref c);
                    switch (c)
                    {
                        case "1":
                            CreateBankAccount();
                            break;
                        case "2": 
                            ChangeName();
                            break;
                        case "3": 
                            ChangeBalance();
                            break;
                        case "4": 
                            ShowAccounts();
                            break;
                        case "5":
                            ShowAccount();
                            break;
                        case "6":
                            DeleteAccount();
                            break;
                    }
                    break;
                case "2": break;
                case "3": break;
                case "4": break;
                case "5": break;
                case "6": break;
            }
            break;
        }
    }

    private BankAccount CreateBankAccount()
    {
        Console.Write("Enter account name: ");
        var name = Console.ReadLine();
        while (name == null || string.IsNullOrWhiteSpace(name))
        {
            PrintWithColor("Input correct name", ConsoleColor.Red);
            name = Console.ReadLine();
        }

        Console.WriteLine("Enter account balance: ");
        var balance = Console.ReadLine();
        while (!decimal.TryParse(balance, out var balanceDecimal) || decimal.Parse(balance) < 0)
        {
            PrintWithColor("Input correct balance", ConsoleColor.Red);
            balance = Console.ReadLine();
        }
        var bal = decimal.Parse(balance);
        return _bankAccountFacade.CreateAccount(name, bal);
    }

    private void ChangeName()
    {
        Console.WriteLine("Enter account id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        Console.Write("Enter account name: ");
        var name = Console.ReadLine();
        while (name == null || string.IsNullOrWhiteSpace(name))
        {
            PrintWithColor("Input correct name", ConsoleColor.Red);
            name = Console.ReadLine();
        }
        _bankAccountFacade.ChangeName(guid, name);
    }

    private void ChangeBalance()
    {
        Console.WriteLine("Enter account id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        
        Console.WriteLine("Enter the amount to deposit or withdraw");
        var amount = Console.ReadLine();
        while (!decimal.TryParse(amount, out _))
        {
            PrintWithColor("Input correct amount", ConsoleColor.Red);
            amount = Console.ReadLine();
        }
        var am = decimal.Parse(amount);
        if (am >= 0)
        {
            _bankAccountFacade.IncreaseBalance(guid, am);
        }
        else
        {
            _bankAccountFacade.DecreaseBalance(guid, am);
        }
    }

    private void ShowAccounts()
    {
        Console.WriteLine("Existing accounts:");
        var accounts = _bankAccountFacade.GetAll();
        foreach (var a in accounts)
        {
            Console.WriteLine($"AccountID: {a.Id}, \nName: {a.Name}, \nBalance: {a.Balance}\n=============");
        }
    }

    private void ShowAccount()
    {
        Console.WriteLine("Enter account id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        var a = _bankAccountFacade.GetById(guid);
        if (a != null)
        {
            Console.WriteLine($"AccountID: {a.Id}, \nName: {a.Name}, \nBalance: {a.Balance}");
        }
        else
        {
            Console.WriteLine("Account not found");
        }
    }

    private void DeleteAccount()
    {
        Console.WriteLine("Enter account id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        _bankAccountFacade.Delete(guid);
    }
    private void PrintAccountMenu()
    {
        Console.WriteLine("1. Create account");
        Console.WriteLine("2. Change account's name");
        Console.WriteLine("3. Change account balance");
        Console.WriteLine("4. Show all accounts");
        Console.WriteLine("5. Get account by id");
        Console.WriteLine("6. Delete account");
    }
    
    private void PrintMenu()
    {
        PrintWithColor("Finance Tracker App", ConsoleColor.Green);
        Console.WriteLine("Choose an option");
        Console.WriteLine("1. Bank account");
        Console.WriteLine("2. Category");
        Console.WriteLine("3. Operation");
        Console.WriteLine("4. Analytics");
        Console.WriteLine("5. Import/Export");
        PrintWithColor("6. Exit", ConsoleColor.Red);
    }

    private void CheckInput(ref string c)
    {
        while (!int.TryParse(c, out int result) || int.Parse(c) > 6 || int.Parse(c) < 0)
        {
            PrintWithColor("Please, input correct value", ConsoleColor.Red);
            c = Console.ReadLine();
        }
    }
    
    
    private static void PrintWithColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}