// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Linq;
using BLL;

void MainMeny()
{
    Console.WriteLine("What would you Like to do: ");
    Console.WriteLine(" 1. Get all Customers");
    Console.WriteLine(" 2. Get Customer by Name (Not Specific)");
    Console.WriteLine(" 3. Get Customer by Id (Specific)");
    Console.WriteLine(" 4. Add Customer");
    Console.WriteLine(" 5. Edit Customer");
    Console.WriteLine(" 6. Add new Account");
    Console.WriteLine(" 7. Take loan for specific account");
    Console.WriteLine(" 8. Give money to specific account");
    Console.WriteLine(" 9. Pay off loan for specific account");
    Console.WriteLine("10. Send a salery to everyone");
    Console.WriteLine("11. Add Customer to Account");
    Console.WriteLine("12. Remove Customer to Account");
    Console.WriteLine("13. Exit");
    Console.Write("Please choose a number (1-11): ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            GetAllCustomers();
            break;

        case "2":
            GetCustomerByName();
            break;

        case "3":
            GetCustomerById();
            break;

        case "4":
            AddCustomer();
            break;

        case "5":
            EditCustomer();
            break;

        case "6":
            AddAccount();
            break;

        case "7":
            takeLoanForAccount();
            break;

        case "8":
            giveMoneyToAccount();
            break;

        case "9":
            payOffLoan();
            break;

        case "10":
            paySalaryToAllCustomersMainAccount();
            break;

        case "11":
            addCustomerToAccount();
            break;

        case "12":
            removeCustomerToAccount();
            break;

        default:
            break;
    }
}

void GetAllCustomers()
{
    List<Dictionary<string, string>>  customers = BLL.Customers.getAllCustomers();
    customers.ForEach(customer =>
    {
        
        Console.WriteLine(customer["name"] + ":");
        Console.WriteLine("    Id: " + customer["id"]);
        Console.WriteLine("    Birthday: " + customer["birthday"]);
        Console.WriteLine("    Saldo: " + customer["saldo"]);
        Console.WriteLine("    Main account: " + customer["mainAccountId"]);
        Console.WriteLine("");

        List<Dictionary<string, string>> customerAccounts = Accounts.GetAccountByCustomerId(customer["id"]);
        customerAccounts.ForEach(account =>
        {
            Console.WriteLine("        Id: " + account["id"]);
            Console.WriteLine("        customerId: " + account["customerId"]);
            Console.WriteLine("        Account name: " + account["name"]);
            Console.WriteLine("        Current loan: " + account["loan"]);
            Console.WriteLine("        Saldo: " + account["mainSaldo"]);
            Console.WriteLine("");
        });
        Console.WriteLine("");
    });
    Console.WriteLine("");
    MainMeny();
}

void GetCustomerByName()
{
    Console.Write("What name would you like to look for: ");
    string name = Console.ReadLine();
    Dictionary<string, string> customer = BLL.Customers.GetCustomerByName(name);

    Console.WriteLine(customer["name"] + ":");
    Console.WriteLine("    Id: " + customer["id"]);
    Console.WriteLine("    Birthday: " + customer["birthday"]);
    Console.WriteLine("    Saldo: " + customer["saldo"]);
    Console.WriteLine("    Main account: " + customer["mainAccountId"]);
    Console.WriteLine("");

    List<Dictionary<string, string>> customerAccounts = Accounts.GetAccountByCustomerId(customer["id"]);
    customerAccounts.ForEach(account =>
    {
        Console.WriteLine("        Id: " + account["id"]);
        Console.WriteLine("        customerId: " + account["customerId"]);
        Console.WriteLine("        Account name: " + account["name"]);
        Console.WriteLine("        Current loan: " + account["loan"]);
        Console.WriteLine("        Saldo: " + account["mainSaldo"]);
        Console.WriteLine("");
    });
    Console.WriteLine("");
    MainMeny();
}

void GetCustomerById()
{
    Console.Write("What id would you like to look for: ");
    string id = Console.ReadLine();
    Dictionary<string, string> customer = BLL.Customers.GetCustomerById(id);

    Console.WriteLine(customer["id"] + ":");
    Console.WriteLine("    Name: " + customer["name"]);
    Console.WriteLine("    Birthday: " + customer["birthday"]);
    Console.WriteLine("    Saldo: " + customer["saldo"]);
    Console.WriteLine("    Main account: " + customer["mainAccountId"]);
    Console.WriteLine("");

    List<Dictionary<string, string>> customerAccounts = Accounts.GetAccountByCustomerId(customer["id"]);
    customerAccounts.ForEach(account =>
    {
        Console.WriteLine("        Id: " + account["id"]);
        Console.WriteLine("        customerId: " + account["customerId"]);
        Console.WriteLine("        Account name: " + account["name"]);
        Console.WriteLine("        Current loan: " + account["loan"]);
        Console.WriteLine("        Saldo: " + account["mainSaldo"]);
        Console.WriteLine("");
    });
    Console.WriteLine("");
    MainMeny();
}

void AddCustomer()
{
    Console.Write("What is the full name of the new customer (Fx. Marcus Bager): ");
    string name = Console.ReadLine();

    Console.Write("What is the birthday for the new customer (Fx. 27-12-04): ");
    string birthday = Console.ReadLine();
    BLL.Customers customer = new BLL.Customers(name, birthday);
    Console.WriteLine(customer.Name + ":");
    Console.WriteLine("    Id: " + customer.Id);
    Console.WriteLine("    Birthday: " + customer.BirthDay);
    Console.WriteLine("    Saldo: " + customer.Saldo);
    Console.WriteLine("    Main account: " + customer.MainAccountId);
    Console.WriteLine("");

    List<Dictionary<string, string>> customerAccounts = Accounts.GetAccountByCustomerId(customer.Id);
    customerAccounts.ForEach(account =>
    {
        Console.WriteLine("        Id: " + account["id"]);
        Console.WriteLine("        customerId: " + account["customerId"]);
        Console.WriteLine("        Account name: " + account["name"]);
        Console.WriteLine("        Current loan: " + account["loan"]);
        Console.WriteLine("        Saldo: " + account["mainSaldo"]);
        Console.WriteLine("");
    });
    Console.WriteLine("");

    MainMeny();
}

void EditCustomer()
{
    Console.Write("What is the id of the customer you wish to edit (Fx. 1): ");
    string id = Console.ReadLine();

    try
    {
        int.Parse(id);
    }
    catch (Exception e)
    {
        Console.WriteLine();
        Console.WriteLine(e.Message);
        Console.WriteLine();
        EditCustomer();
        return;
    }

    Console.Write("What is the new full name of the customer (Leave blank to not edit): ");
    string name = Console.ReadLine();

    Console.Write("What is the new birthday for the customer (Leave blank to not edit): ");
    string birthday = Console.ReadLine();

    Console.Write("What is the new saldo for the customer (Leave blank to not edit): ");
    string saldo = Console.ReadLine();

    Console.WriteLine();
    Console.WriteLine(BLL.Customers.editCustomer(id, name, birthday, saldo));
    Console.WriteLine();
    MainMeny();
}

void AddAccount()
{
    Console.Write("What is the name of the new bank account (Fx. Marcus Bager's account): ");
    string accountName = Console.ReadLine();

    Console.Write("What is the customersId for the new account (Fx. 1): ");
    string customersId = Console.ReadLine();

    try
    {
        int.Parse(customersId);
    } catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        return;
    }

    new Accounts(accountName, customersId);
    MainMeny();
}

void getAllAccounts()
{
    List<Dictionary<string, string>> customerAccounts = Accounts.GetAllAccounts();
    customerAccounts.ForEach(account =>
    {
        Console.WriteLine("        id: " + account["id"]);
        Console.WriteLine("        customerId: " + account["customerId"]);
        Console.WriteLine("        Account name: " + account["name"]);
        Console.WriteLine("        Current loan: " + account["loan"]);
        Console.WriteLine("        Saldo: " + account["mainSaldo"]);
        Console.WriteLine("");
    });
    Console.WriteLine("");
    MainMeny();
}

void takeLoanForAccount()
{
    Console.Write("What is the id of the account you would like to take a loan on: ");
    string id;
    try
    {
        id = int.Parse(Console.ReadLine()).ToString();
    } catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        takeLoanForAccount();
        return;
    }

    Console.Write("How big of a loan would you like to take (1-9999): ");
    int value;
    try
    {
        value = int.Parse(Console.ReadLine());
        if (value > 9999 || value < 1) { throw new Exception("The number you wrote is not valid!"); }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        takeLoanForAccount();
        return;
    }

    Accounts.takeLoan(value, id);
    MainMeny();
    return;
}

void giveMoneyToAccount()
{
    Console.Write("What is the id of the account you would like to give money to: ");
    string id;
    try
    {
        id = int.Parse(Console.ReadLine()).ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        takeLoanForAccount();
        return;
    }

    Console.Write("What amount would you like to add to the account (1-9999): ");
    int value;
    string stringValue;
    try
    {
        value = int.Parse(Console.ReadLine());
        if (value > 9999 || value < 1) { throw new Exception("The number you wrote is not valid!"); }
        stringValue = value.ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        giveMoneyToAccount();
        return;
    }

    Accounts.addMoneyToSaldo(stringValue, id);
    MainMeny();
    return;
}

void payOffLoan()
{
    Console.Write("What is the id of the account you would like to payoff loan: ");
    string id;
    try
    {
        id = int.Parse(Console.ReadLine()).ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        takeLoanForAccount();
        return;
    }

    int currentLoan = Accounts.getCurrentLoan(id);
    if (currentLoan == 0)
    {
        Console.WriteLine("This account do not currenly have a loan!");
        Console.WriteLine("");
        MainMeny();
        return;
    }

    int currentSaldo = Accounts.getCurrentSaldo(id);
    if (currentSaldo == 0)
    {
        Console.WriteLine("This account do not have enough money to pay off any loans!");
        Console.WriteLine("Saldo = 0");
        Console.WriteLine("");
        MainMeny();
        return;
    }

    Console.Write("What amount would you like to payoff (1-" + currentLoan + "): ");
    int value;
    try
    {
        value = int.Parse(Console.ReadLine());
        if (value > currentLoan || value < 1) { throw new Exception("The number you wrote is not valid!"); }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        giveMoneyToAccount();
        return;
    }

    Accounts.payOffLoan(value, id);
    MainMeny();
    return;
}

void paySalaryToAllCustomersMainAccount()
{
    Console.Write("How much would you like to pay everyone in salery?: ");
    string value = int.Parse(Console.ReadLine()).ToString();

    List<Dictionary<string, string>> customers = BLL.Customers.getAllCustomers();
    customers.ForEach(customer =>
    {
        Accounts.addMoneyToSaldo(value, customer["mainAccountId"]);
        Console.WriteLine("");
    });
    MainMeny();
}

void addCustomerToAccount()
{
    Console.Write("What account id would you like to add customer to: ");
    string id;
    try
    {
        id = int.Parse(Console.ReadLine()).ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        addCustomerToAccount();
        return;
    }

    string name = Accounts.GetAccountById(id)["name"];
    Console.Write("What customer id should be added to " + name + ": ");

    string customerId;
    try
    {
        customerId = int.Parse(Console.ReadLine()).ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        addCustomerToAccount();
        return;
    }

    Console.WriteLine(Accounts.addCustomerToAccount(customerId, id));
    Console.WriteLine("");
    MainMeny();
}

void removeCustomerToAccount()
{
    Console.Write("What account id would you like to remove a customer from: ");
    string id;
    try
    {
        id = int.Parse(Console.ReadLine()).ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        addCustomerToAccount();
        return;
    }

    string name = Accounts.GetAccountById(id)["name"];
    Console.Write("What customer id should be removed from" + name + ": ");

    string customerId;
    try
    {
        customerId = int.Parse(Console.ReadLine()).ToString();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("");
        addCustomerToAccount();
        return;
    }

    Console.WriteLine(Accounts.removeCustomerToAccount(customerId, id));
    Console.WriteLine("");
    MainMeny();
}




MainMeny();