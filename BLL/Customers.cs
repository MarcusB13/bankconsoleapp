using System;
using System.Xml.Linq;
using BLL;
using DAL;

namespace BLL
{
	public class Customers
	{
        private JsonHandler _JsonHandler;
        private Accounts _AccountHandler;
        public Customers(JsonHandler jsonHandler)
		{
            _JsonHandler = jsonHandler;
        }

        public Customer CreateCustomer(string name, string birthDay)
        {
            int length = _JsonHandler.Read<Customer>().Count();
            
            string id = (length + 1).ToString();
            Customer customer = new Customer { name = name, birthDay = birthDay, id=id };

            List<Customer> customers = _JsonHandler.Read<Customer>();
            customers.Add(customer);

            _JsonHandler.Write(customers);
            return customer;
        }

        public Customer? editCustomer(string id, string name, string birthday, string mainAccountId)
        {
            try
            {
                List<Customer> customers = _JsonHandler.Read<Customer>();
                Customer customer = customers.FirstOrDefault(e => e.id == id);

                if (name.Split(" ").Length < 2)
                {
                    // Then we don't edit the name
                    name = customer.name;
                }

                customer.name = name;
                customer.birthDay = birthday == "" ? customer.birthDay : birthday;
                customer.mainAccountId = mainAccountId == "" ? customer.mainAccountId : mainAccountId;

                _JsonHandler.Write(customers);
                return customer;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public List<Customer> getAllCustomers()
        {
            List<Customer> customers = _JsonHandler.Read<Customer>();
            return customers;
        }

        public Customer GetCustomerByName(string name)
        {
            List<Customer> customers = _JsonHandler.Read<Customer>();
            Customer customer = customers.FirstOrDefault(e => e.name == name);

            return customer;
        }

        public Customer GetCustomerById(string id)
        {
            List<Customer> customers = _JsonHandler.Read<Customer>();
            Customer customer = customers.FirstOrDefault(e => e.id == id);

            return customer;
        }


    }
}

