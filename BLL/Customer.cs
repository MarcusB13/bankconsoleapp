using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using DAL;


namespace BLL
{
    public class Customer
    {
        public string? name { get; set; }
        public string? id { get; set; }
        public string? birthDay { get; set; }
        public string? mainAccountId { get; set; }
    }
}

