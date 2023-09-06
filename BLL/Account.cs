using System;
using System.Xml.Linq;
using DAL;

namespace BLL
{
	public class Account
	{
        public string customerId { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public int mainSaldo { get; set; }
        public int loan { get; set; }
    }
}

