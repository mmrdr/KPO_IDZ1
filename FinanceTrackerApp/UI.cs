using System.Text.RegularExpressions;
using FinanceTrackerApp.Domain.Abstractions.Export;
using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Export;
using FinanceTrackerApp.Domain.Import;
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

    public UI(
        IBankAccountFacade bankAccountFacade,
        ICategoryFacade categoryFacade,
        IOperationFacade operationFacade,
        IAnalyticFacade analyticFacade
    )
    {
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
        _analyticFacade = analyticFacade;
    }
    
    public void RunApp()
    {
        while (true)
        {
            PrintMenu();
            var c = Console.ReadLine();
            CheckInput(ref c, 0, 6);
            switch (c)
            {
                case "1":
                    PrintAccountMenu();
                    c = Console.ReadLine();
                    CheckInput(ref c, 0, 6);
                    switch (c)
                    {
                        case "1":
                            CreateBankAccount();
                            break;
                        case "2": 
                            ChangeName(Option.Bank);
                            break;
                        case "3": 
                            ChangeBalance();
                            break;
                        case "4": 
                            ShowAll(Option.Bank);
                            break;
                        case "5":
                            ShowByID(Option.Bank);
                            break;
                        case "6":
                            Delete(Option.Bank);
                            break;
                    }
                    break;
                case "2":
                    PrintCategoryMenu();
                    c = Console.ReadLine();
                    CheckInput(ref c, 0, 5);
                    switch (c)
                    {
                        case "1":
                            CreateCategory();
                            break;
                        case "2":
                            ChangeName(Option.Category);
                            break;
                        case "3":
                            ShowAll(Option.Category);
                            break;
                        case "4":
                            ShowByID(Option.Category);
                            break;
                        case "5":
                            Delete(Option.Category);
                            break;
                    }
                    break;
                case "3": 
                    PrintOperationMenu();
                    c = Console.ReadLine();
                    CheckInput(ref c, 0, 4);
                    switch (c)
                    {
                        case "1":
                            CreateOperation();
                            break;
                        case "2":
                            ShowAll(Option.Operation);
                            break;
                        case "3":
                            ShowByID(Option.Operation);
                            break;
                        case "4":
                            Delete(Option.Operation);
                            break;
                    }
                    break;
                case "4":
                    PrintAnalyticsMenu();
                    c = Console.ReadLine();
                    CheckInput(ref c, 0, 3);
                    switch (c)
                    {
                        case "1":
                            CalculateIncomeExpenseDifference();
                            break;
                        case "2": 
                            GroupByCategory();
                            break;
                        case "3": 
                            GetTotal();
                            break;
                    }
                    break;
                case "5":
                    PrintDataTransferMenu();
                    c = Console.ReadLine();
                    CheckInput(ref c, 0, 2);
                    switch (c)
                    {
                        case "1": 
                            PrintExportMenu();
                            break;
                        case "2":
                            PrintImportMenu();
                            break;
                    }
                    break;
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

    private Category CreateCategory()
    {
        Console.WriteLine("Income/Expense?(type[i/d]");
        var c = Console.ReadLine();
        while (c == null || string.IsNullOrWhiteSpace(c) || (c != "i" && c != "d"))
        {
            PrintWithColor("Input correct type", ConsoleColor.Red);
            c = Console.ReadLine();
        }
        Console.Write("Enter category name: ");
        var category = Console.ReadLine();
        while (category == null || string.IsNullOrWhiteSpace(category))
        {
            PrintWithColor("Input correct category", ConsoleColor.Red);
            category = Console.ReadLine();
        }
        
        return _categoryFacade.CreateCategory(c == "i" ? OperationType.Income : OperationType.Expense, category);
    }

    private Operation? CreateOperation()
    {
        Console.WriteLine("Income/Expense?(type[i/d])");
        var c = Console.ReadLine();
        while (c == null || string.IsNullOrWhiteSpace(c) || (c != "i" && c != "d"))
        {
            PrintWithColor("Input correct type", ConsoleColor.Red);
            c = Console.ReadLine();
        }
        Console.WriteLine("Enter account id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        var account = _bankAccountFacade.GetById(guid);
        if (account == null)
        {
            PrintWithColor("Account not found", ConsoleColor.Red);
            return null;
        }

        string? amount = null;
        if (c == "i")
        {
            Console.WriteLine("Enter the amount to deposit");
            amount = Console.ReadLine();
            while (!decimal.TryParse(amount, out _) || decimal.Parse(amount) < 0)
            {
                PrintWithColor("Input correct amount", ConsoleColor.Red);
                amount = Console.ReadLine();
            }
            _bankAccountFacade.IncreaseBalance(guid, int.Parse(amount));
        }
        else
        {
            Console.WriteLine("Enter the amount to withdraw");
            amount = Console.ReadLine();
            while (!decimal.TryParse(amount, out _) || decimal.Parse(amount) < 0)
            {
                PrintWithColor("Input correct amount", ConsoleColor.Red);
                amount = Console.ReadLine();
            }
            _bankAccountFacade.DecreaseBalance(guid, int.Parse(amount));
        }
        var am = decimal.Parse(amount);
        Console.WriteLine("Enter description if you want, else press enter");
        var description = Console.ReadLine();
        Console.WriteLine("Enter category id: ");
        var cId = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            cId = Console.ReadLine();
        }
        var cGuid = Guid.Parse(cId);
        var category = _categoryFacade.GetById(cGuid);
        if (category == null)
        {
            PrintWithColor("Category not found", ConsoleColor.Red);
            return null;
        }
        return _operationFacade.CreateOperationById(
            c == "i" ? OperationType.Income : OperationType.Expense, 
            guid, 
            am, 
            description, 
            cGuid
            );
    }

    private void ChangeName(Option o)
    {
        Console.WriteLine("Enter id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        switch (o)
        {
            case Option.Bank:
                var a = _bankAccountFacade.GetById(guid);
                if (a == null)
                {
                    PrintWithColor("Account not found", ConsoleColor.Red);
                    return;
                }
                Console.Write("Enter new name: ");
                var name = Console.ReadLine();
                while (name == null || string.IsNullOrWhiteSpace(name))
                {
                    PrintWithColor("Input correct name", ConsoleColor.Red);
                    name = Console.ReadLine();
                }
                _bankAccountFacade.ChangeName(guid, name);
                break;
            case Option.Category:
                var c = _categoryFacade.GetById(guid);
                if (c == null)
                {
                    PrintWithColor("Category not found", ConsoleColor.Red);
                    return;
                }
                Console.Write("Enter new name: ");
                name = Console.ReadLine();
                while (name == null || string.IsNullOrWhiteSpace(name))
                {
                    PrintWithColor("Input correct name", ConsoleColor.Red);
                    name = Console.ReadLine();
                }
                _categoryFacade.ChangeName(guid, name);
                break;
        }
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
        var e = _bankAccountFacade.GetById(guid);
        if (e == null)
        {
            PrintWithColor("Account not found", ConsoleColor.Red);
            return;
        }
        
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

    private void ShowAll(Option o)
    {
        switch (o)
        {
            case Option.Bank:
                Console.WriteLine("Existing accounts:");
                var accounts = _bankAccountFacade.GetAll();
                foreach (var a in accounts)
                {
                    Console.WriteLine($"AccountID: {a.Id}, \nName: {a.Name}, \nBalance: {a.Balance}\n=============");
                }

                break;
            case Option.Category:
                Console.WriteLine("Existing categories:");
                var categories = _categoryFacade.GetAll();
                foreach (var category in categories)
                {
                    Console.WriteLine($"CategoryID: {category.Id}, \nName: {category.Name}, \nType: {category.Type}");
                }
                break;
            case Option.Operation:
                Console.WriteLine("Existing operations:");
                var operations = _operationFacade.GetAll();
                foreach (var operation in operations)
                {
                    Console.WriteLine($"OperationID: {operation.Id}," +
                                      $" \nAccountID: {operation.BankAccountId}, " +
                                      $" \nType: {operation.Type.GetDescription()}, " +
                                      $" \nAmount: {operation.Amount}, " +
                                      $" \nDate: {operation.Date}, " +
                                      $" \nDescription: {operation.Description}," +
                                      $" \nCategoryID: {operation.CategoryId}");
                }
                break;
        }

    }

    private void ShowByID(Option o)
    {
        Console.WriteLine("Enter account id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        switch (o)
        {
            case Option.Bank:
                var bank = _bankAccountFacade.GetById(guid);
                if (bank != null)
                {
                    Console.WriteLine($"AccountID: {bank.Id}, \nName: {bank.Name}, \nBalance: {bank.Balance}");
                }
                else
                {
                    Console.WriteLine("Account not found");
                }
                break;
            case Option.Category:
                var category = _categoryFacade.GetById(guid);
                if (category != null)
                {
                    Console.WriteLine($"CategoryID: {category.Id}, \nName: {category.Name}, \nType: {category.Type}");
                }
                else
                {
                    Console.WriteLine("Category not found");
                }
                break;
            case Option.Operation:
                var operation = _operationFacade.GetById(guid);
                if (operation != null)
                {
                    Console.WriteLine($"OperationID: {operation.Id}," +
                                      $" \nAccountID: {operation.BankAccountId}, " +
                                      $" \nType: {operation.Type.GetDescription()}, " +
                                      $" \nAmount: {operation.Amount}, " +
                                      $" \nDate: {operation.Date}, " +
                                      $" \nDescription: {operation.Description}," +
                                      $" \nCategoryID: {operation.CategoryId}");
                }
                else
                {
                    Console.WriteLine("Category not found");
                }
                break;
        }
    }

    private void Delete(Option o)
    {
        Console.WriteLine("Enter id: ");
        var id = Console.ReadLine();
        while (!Guid.TryParse(id, out _))
        {
            PrintWithColor("Input correct id", ConsoleColor.Red);
            id = Console.ReadLine();
        }
        var guid = Guid.Parse(id);
        switch (o)
        {
            case Option.Bank:
                _bankAccountFacade.Delete(guid);
                break;
            case Option.Category:
                _categoryFacade.Delete(guid);
                break;
            case Option.Operation:
                _operationFacade.Delete(guid);
                break;
        }
    }

    private void CalculateIncomeExpenseDifference()
    {
        var dates = GetInput();
        var start = dates[0];
        var end = dates[1];
        var difference = _analyticFacade.CalculateIncomeExpenseDifference(start, end);
        Console.WriteLine(difference);
    }

    private void GroupByCategory()
    {
        var dates = GetInput();
        var start = dates[0];
        var end = dates[1];
        var dict = _analyticFacade.GroupByCategory(start, end);
        foreach (var kv in dict)
        {
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }
    }

    private void GetTotal()
    {
        var dates = GetInput();
        var start = dates[0];
        var end = dates[1];
        var dict = _analyticFacade.GetTotalIncomeAndExpense(start, end);
        foreach (var kv in dict)
        {
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }
    }

    private DateTime[] GetInput()
    {
        var result = new DateTime[2];
        string pattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";
        var regex = new Regex(pattern);
        Console.WriteLine("Type start date[yyyy-MM-dd]");
        var startDate = Console.ReadLine();
        while (!DateTime.TryParse(startDate, out DateTime date) || !regex.IsMatch(startDate))
        {
            PrintWithColor("Input correct data", ConsoleColor.Red);
            startDate = Console.ReadLine();
        }
        Console.WriteLine("Type end date[yyyy-MM-dd]");
        var endDate = Console.ReadLine();
        while (!DateTime.TryParse(endDate, out DateTime date) || !regex.IsMatch(endDate))
        {
            PrintWithColor("Input correct data", ConsoleColor.Red);
            endDate = Console.ReadLine();
        }
        result[0] = DateTime.Parse(startDate).ToUniversalTime();
        result[1] = DateTime.Parse(endDate).ToUniversalTime();
        return result;
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

    private void PrintCategoryMenu()
    {
        Console.WriteLine("1. Create category");
        Console.WriteLine("2. Change category's name");
        Console.WriteLine("3. Show all categories");
        Console.WriteLine("4. Get category by id");
        Console.WriteLine("5. Delete category");
    }

    private void PrintOperationMenu()
    {
        Console.WriteLine("1. Create operation");
        Console.WriteLine("2. Show all operations");
        Console.WriteLine("3. Get operation by id");
        Console.WriteLine("4. Delete operation");
    }

    private void PrintAnalyticsMenu()
    {
        Console.WriteLine("1. Calculate income and expense difference");
        Console.WriteLine("2. Show group by category");
        Console.WriteLine("3. Get total income and expense");
    }

    private void PrintDataTransferMenu()
    {
        Console.WriteLine("1. Export");
        Console.WriteLine("2. Import");
    }

    private void PrintExportMenu()
    {
        Console.WriteLine("Choose format: 1 - JSON, 2 - CSV, 3 - YAML");
        var c = Console.ReadLine();
        DataExporter? _dataExporter = c switch
        {
            "1" => new JsonExporter(),
            "2" => new CsvExporter(),
            "3" => new YamlExporter(),
            _ => null
        };
        if (_dataExporter == null)
        {
            PrintWithColor("Incorrect input", ConsoleColor.Red);
            return;
        }
        // костыль, на этом этапе импортер не нужен
        DataImporter n = new CsvDataImporter();
        var dataTransferFacade = new DataTransferFacade(
            n, 
            _dataExporter, 
            _bankAccountFacade, 
            _categoryFacade, 
            _operationFacade);
        Console.Write("Input file path: ");
        var path = Console.ReadLine();
        while (!File.Exists(path))
        {
            PrintWithColor("Input correct file path", ConsoleColor.Red);
            path = Console.ReadLine();
        }
        try
        {
            dataTransferFacade.ExportData(path);
            PrintWithColor("Exported!", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            PrintWithColor($"Export error:{ex.Message}", ConsoleColor.Red);
        }
    }

    private void PrintImportMenu()
    {
        Console.WriteLine("Choose format: 1 - JSON, 2 - CSV, 3 - YAML");
        var c = Console.ReadLine();
        DataImporter? _dataImporter = c switch
        {
            "1" => new JsonDataImporter(),
            "2" => new CsvDataImporter(),
            "3" => new YamlDataImporter(),
            _ => null
        };
        if (_dataImporter == null)
        {
            PrintWithColor("Incorrect input", ConsoleColor.Red);
            return;
        }
        // костыль, на этом этапе экспортер не нужен
        DataExporter n = new CsvExporter();
        var dataTransferFacade = new DataTransferFacade(
            _dataImporter, 
            n, 
            _bankAccountFacade, 
            _categoryFacade, 
            _operationFacade);
        Console.Write("Input file path: ");
        var path = Console.ReadLine();
        while (!File.Exists(path))
        {
            PrintWithColor("Input correct file path", ConsoleColor.Red);
            path = Console.ReadLine();
        }

        try
        {
            dataTransferFacade.ImportData(path);
            PrintWithColor("Imported!", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            PrintWithColor($"Import error:{ex.Message}", ConsoleColor.Red);
        }
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

    private void CheckInput(ref string c, int l, int r)
    {
        while (!int.TryParse(c, out int result) || int.Parse(c) < l || int.Parse(c) > r)
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

enum Option
{
    Bank,
    Category,
    Operation
}