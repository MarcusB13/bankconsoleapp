using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL
{
    internal class BaseDatabase
    {
        static public string readFile(string filename)
        {
            using StreamReader reader = new(filename);
            var json = reader.ReadToEnd();
            return json;
        }

        static public void writeToFile(string data, string filename)
        {
            File.WriteAllText(filename, data);
        }
    }


	public class CustomerDatabasse
	{
        

        static private List<Dictionary<string, string>> Customers()
        {
            var json = BaseDatabase.readFile("/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/customers.json");
            List<Dictionary<string, string>> data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            return data;
        }


        static public void addCustomer(Dictionary<string, string> json)
        {
            List<Dictionary<string, string>> data = Customers();
            data.Add(json);
            string toWrite = JsonSerializer.Serialize(data);
            BaseDatabase.writeToFile(toWrite, "/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/customers.json");
        }

        static public void editCustomer(string id, string name, string birthday, string saldo)
        {
            List<Dictionary<string, string>> data = Customers();
            int toEditIndex= data.FindIndex(e => e["id"] == id);
            Dictionary<string, string> toEdit = data.Find(e => e["id"] == id);

            data[toEditIndex] = new Dictionary<string, string>
            {
                { "name", name == "" ? toEdit["name"] : name},
                { "birthday", birthday == "" ? toEdit["birthday"] : birthday },
                { "id", toEdit["id"] },
                { "saldo",  saldo == "" ? toEdit["saldo"] : saldo },
                { "mainAccountId", toEdit["mainAccountId"] },
            };
            string toWrite = JsonSerializer.Serialize(data);
            BaseDatabase.writeToFile(toWrite, "/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/customers.json");
        }

        static public string nextId()
        {
            List<Dictionary<string, string>> data = new();
            var json = File.ReadAllText("/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/customers.json");
            data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            int length = data.Count + 1;
            return length.ToString();
        }

        static public Dictionary<string, string> getCustomerById(string id)
        {
            List<Dictionary<string, string>> customers = Customers();
            Dictionary<string, string> customer = customers.Find(e => e["id"] == id);
            return customer;
        }

        static public Dictionary<string, string> getCustomerByName(string name)
        {
            List<Dictionary<string, string>> customers = Customers();
            Dictionary<string, string> customer = customers.Find(e => e["name"] == name);
            return customer;
        }

        static public List<Dictionary<string, string>> getAllCustomer()
        {
            List<Dictionary<string, string>> customers = Customers();
            return customers;
        }
    }


    public class AccountsDatabase
    {
        static public string nextId()
        {
            List<Dictionary<string, string>> data = new();
            var json = BaseDatabase.readFile("/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/accounts.json");
            data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            int length = data.Count + 1;
            return length.ToString();
        }

        static private List<Dictionary<string, string>> Accounts()
        {
            var json = BaseDatabase.readFile("/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/accounts.json");
            List<Dictionary<string, string>> data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            return data;
        }

        static public List<Dictionary<string, string>> getAllAccounts()
        {
            List<Dictionary<string, string>> customers = Accounts();
            return customers;
        }

        static public void addAccount(Dictionary<string, string> json)
        {
            List<Dictionary<string, string>> data = Accounts();
            data.Add(json);
            string toWrite = JsonSerializer.Serialize(data);
            BaseDatabase.writeToFile(toWrite, "/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/accounts.json");
        }

        static public void editAccount(string name, string id, string loan, string mainSaldo, string customerIds)
        {
            List<Dictionary<string, string>> data = Accounts();
            int toEditIndex = data.FindIndex(e => e["id"] == id);
            Dictionary<string, string> toEdit = data.Find(e => e["id"] == id);

            data[toEditIndex] = new Dictionary<string, string> 
            {
                { "id",  toEdit["id"]},
                { "customerId", name == "" ? toEdit["customerIds"] : name },
                { "name", name == "" ? toEdit["name"] : name },
                { "loan", loan == "" ? toEdit["loan"] : loan },
                { "mainSaldo", mainSaldo == "" ? toEdit["mainSaldo"] : mainSaldo },
            };
            string toWrite = JsonSerializer.Serialize(data);
            BaseDatabase.writeToFile(toWrite, "/Users/marcusbager/Projects/BankAppBasicProgramming/DAL/accounts.json");
        }

        static public List<Dictionary<string, string>> getAccountByCustomerId(string id)
        {
            List<Dictionary<string, string>> accounts = Accounts();
            accounts = accounts.FindAll(e => e["customerId"].Split(",").ToList().Contains(id));
            return accounts;
        }

        static public Dictionary<string, string> getAccountById(string id)
        {
            List<Dictionary<string, string>> accounts = Accounts();
            Dictionary<string, string>  account = accounts.Find(e => e["id"] == id);
            return account;
        }
    }
}

