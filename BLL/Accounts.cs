using System;
using System.Xml.Linq;
using DAL;

namespace BLL
{
	public class Accounts
	{
        private string customerId;
        private string name;
        private string id;
        private string mainSaldo;
        private string loan;

        public string CustomerId
        {
            get { return customerId; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Id
        {
            get { return id; }
        }

        public string MainSaldo
        {
            get { return MainSaldo; }
        }

        public string Loan
        {
            get { return loan; }
        }



        public Accounts(string bankName, string customersId)
		{
            customerId = customersId;
            name = bankName;
            loan = "0";
            mainSaldo = "0";
            id = AccountsDatabase.nextId();

            Dictionary<string, string> customerData = new Dictionary<string, string> {
                { "id", id },
                { "customerId", customerId },
                { "name", name },
                { "loan", loan },
                { "mainSaldo", mainSaldo },
            };

            AccountsDatabase.addAccount(customerData);
        }

        static public string editAccount(string name, string id, string loan, string mainSaldo, string customerId)
        {
            try
            {
                if (loan != "")
                {
                    int.Parse(loan);
                }
                if (mainSaldo != "")
                {
                    int.Parse(mainSaldo);
                }

                if(customerId != ""){
                    customerId.Split(",").ToList();
                }

                AccountsDatabase.editAccount(name, id, loan, mainSaldo, customerId);
                return "Edited";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Not editd";
            }
        }

        static public List<Dictionary<string, string>> GetAllAccounts()
        {
            return AccountsDatabase.getAllAccounts();
        }

        static public List<Dictionary<string, string>> GetAccountByCustomerId(string id)
        {
            return AccountsDatabase.getAccountByCustomerId(id);
        }

        static public Dictionary<string, string> GetAccountById(string id)
        {
            return AccountsDatabase.getAccountById(id);
        }

        static public void addMoneyToSaldo( string value, string id)
        {
            editAccount("", id, "", value, "");
        }

        static public int getCurrentLoan(string id)
        {
            int loan = int.Parse(AccountsDatabase.getAccountById(id)["loan"]);
            return loan;
        }

        static public int getCurrentSaldo(string id)
        {
            int mainSaldo = int.Parse(AccountsDatabase.getAccountById(id)["mainSaldo"]);
            return mainSaldo;
        }

        static public void takeLoan(int value, string id)
        {
            Dictionary<string, string> account = AccountsDatabase.getAccountById(id);
            int loan = int.Parse(account["loan"]);
            int mainSaldo = int.Parse(account["mainSaldo"]);

            loan += value;
            mainSaldo += value;

            string stringLoan = loan.ToString();
            string stringMainSaldo = mainSaldo.ToString();
            editAccount("", id, stringLoan, stringMainSaldo, "");
        }

        static public void payOffLoan(int value, string id)
        {
            Dictionary<string, string> account = AccountsDatabase.getAccountById(id);
            int loan = int.Parse(account["loan"]);
            int mainSaldo = int.Parse(account["mainSaldo"]);

            loan -= value;
            mainSaldo -= value;

            string stringLoan = loan.ToString();
            string stringMainSaldo = mainSaldo.ToString();
            editAccount("", id, stringLoan, stringMainSaldo, "");
        }

        static public string addCustomerToAccount(string customerId, string id)
        {
            Dictionary<string, string> account = AccountsDatabase.getAccountById(id);
            List<string> customerIds = account["customerId"].Split(",").ToList();
            if (customerId.Contains(customerId))
            {
                return "Customer is already a part of this account";
            }

            customerIds.Add(customerId);
            editAccount("", id, "", "", customerId);
            Dictionary<string, string> updatedAccount = AccountsDatabase.getAccountById(id);
            string updatedAccountCustomerIds = updatedAccount["customerId"];


            return "Account now has following customer ids: " + updatedAccountCustomerIds;
        }

        static public string removeCustomerToAccount(string customerId, string id)
        {
            Dictionary<string, string> account = AccountsDatabase.getAccountById(id);
            List<string> customerIds = account["customerId"].Split(",").ToList();
            if (customerId.Contains(customerId))
            {
                return "Customer is not a part of account";
            }

            int index = customerIds.IndexOf(customerId);
            customerIds.RemoveAt(index);
            editAccount("", id, "", "", customerId);
            Dictionary<string, string> updatedAccount = AccountsDatabase.getAccountById(id);
            string updatedAccountCustomerIds = updatedAccount["customerId"];


            return "Account now has following customer ids: " + updatedAccountCustomerIds;
        }
    }
}

