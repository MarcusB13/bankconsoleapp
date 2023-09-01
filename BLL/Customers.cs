using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using DAL;


namespace BLL
{
    public class Customers
    {
        private string name;
        private string id;
        private string birthDay;
        private string saldo;
        private string mainAccountId;

        public string Name
        {
            get { return name;  }
            set { name = value; }
        }

        public string Id
        {
            get { return id; }
        }

        public string MainAccountId
        {
            get { return mainAccountId; }
        }

        public string Saldo
        {
            get { return id; }
        }

        public string BirthDay
        {
            get { return birthDay; }
        }
                
        public Customers(string newName, string newBirthDay)
        {
            name = newName;
            birthDay = newBirthDay;
            if (name.Split(" ")[1] == null)
            {
                throw new NullReferenceException("Customer needs a last name");
            }

            saldo = "0";
            id = CustomerDatabasse.nextId();

            Accounts account = new Accounts("Bank for " + name, id);

            mainAccountId = account.Id;

            Dictionary<string, string> customerData = new Dictionary<string, string> {
                { "id", id },
                { "name", name },
                { "birthday", birthDay },
                { "saldo", saldo },
                { "mainAccountId", mainAccountId },
            };

            CustomerDatabasse.addCustomer(customerData);
        }

        static public string editCustomer(string id, string name, string birthday, string saldo)
        {
            try
            {
                int.Parse(saldo);
                if (name.Split(" ").Length < 2)
                {
                    // Then we don't edit the name
                    name = "";
                }
                CustomerDatabasse.editCustomer(id, name, birthday, saldo);
                return "Edited";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        static public List<Dictionary<string, string>> getAllCustomers()
        {
            return CustomerDatabasse.getAllCustomer();
        }

        static public Dictionary<string, string> GetCustomerByName(string name)
        {
            return CustomerDatabasse.getCustomerByName(name);
        }

        static public Dictionary<string, string> GetCustomerById(string id)
        {
            return CustomerDatabasse.getCustomerById(id);
        }


    }
}

