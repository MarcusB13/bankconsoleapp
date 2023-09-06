using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DAL;

namespace BLL
{
	public class Accounts
	{
        private JsonHandler _JsonHandler;
		public Accounts(JsonHandler jsonHandler)
		{
            _JsonHandler = jsonHandler;

        }

        public Account CreateAccount(string name, string customerId, int loan, int mainSaldo)
        {
            int length = _JsonHandler.Read<Account>().Count();

            string id = (length + 1).ToString();
            Account account = new Account { name = name, id = id, customerId = customerId, loan = loan, mainSaldo = mainSaldo };

            List<Account> accounts = _JsonHandler.Read<Account>();
            accounts.Add(account);

            _JsonHandler.Write(accounts);
            return account;
        }

        public Account? editAccount(string name, string id, int? loan, int? mainSaldo, string customerIds)
        {
            try
            {
                List<Account> accounts = _JsonHandler.Read<Account>();
                Account account = accounts.FirstOrDefault(e => e.id == id);

                account.name = name == "" ? account.name : name;
                account.loan = loan == null ? account.loan : loan;
                account.mainSaldo = mainSaldo == null ? account.mainSaldo : mainSaldo;
                account.customerId = customerIds == null ? account.customerId : customerIds;

                _JsonHandler.Write(accounts);
                return account;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Account>? GetAllAccounts()
        {
            List<Account> accounts = _JsonHandler.Read<Account>();
            return accounts;
        }

        public List<Account>? GetAccountByCustomerId(string id)
        {
            List<Account> accounts = _JsonHandler.Read<Account>();
            List<Account> foundAccounts = accounts.FindAll(e => e.customerId == id);
            return foundAccounts;
        }

        public Account? GetAccountById(string id)
        {
            List<Account> accounts = _JsonHandler.Read<Account>();
            Account account = accounts.FirstOrDefault(e => e.id == id);
            return account;
        }

        public void addMoneyToSaldo(string input, string id)
        {
            int value = int.Parse(input);
            editAccount("", id, null, value, "");
        }

        public int? getCurrentLoan(string id)
        {
            int? loan = GetAccountById(id).loan;
            return loan;
        }

        public int? getCurrentSaldo(string id)
        {
            int? mainSaldo = GetAccountById(id).mainSaldo;
            return mainSaldo;
        }

        public void takeLoan(int value, string id)
        {
            Account account = GetAccountById(id);
            int? loan = account.loan;
            int? mainSaldo = account.mainSaldo;

            loan += value;
            mainSaldo += value;
            editAccount("", id, loan, mainSaldo, "");
        }

        public void payOffLoan(int value, string id)
        {
            Account account = GetAccountById(id);
            int? loan = account.loan;
            int? mainSaldo = account.mainSaldo;

            loan -= value;
            mainSaldo -= value;
            editAccount("", id, loan, mainSaldo, "");
        }

        public string addCustomerToAccount(string customerId, string id)
        {
            Account account = GetAccountById(id);
            List<string> customerIds = account.customerId.Split(",").ToList();
            if (customerIds.Contains(customerId))
            {
                return "Customer is already a part of this account";
            }

            customerIds.Add(customerId);

            string customerIdsString = String.Join(",", customerIds.ToArray());
            editAccount("", id, null, null, customerIdsString);

            Account updatedAccount = GetAccountById(id);
            string updatedAccountCustomerIds = updatedAccount.customerId;
            return "Account now has following customer ids: " + updatedAccountCustomerIds;
        }

        public string removeCustomerToAccount(string customerId, string id)
        {
            Account account = GetAccountById(id);
            List<string> customerIds = account.customerId.Split(",").ToList();
            if (!customerId.Contains(customerId))
            {
                return "Customer is not a part of account";
            }

            int index = customerIds.IndexOf(customerId);
            Console.WriteLine(index);
            Console.WriteLine(customerIds[0]);
            customerIds.RemoveAt(index);

            string customerIdsString = String.Join(",", customerIds.ToArray());
            editAccount("", id, null, null, customerIdsString);

            Account updatedAccount = GetAccountById(id);
            string updatedAccountCustomerIds = updatedAccount.customerId;
            return "Account now has following customer ids: " + updatedAccountCustomerIds;
        }
    }
}

