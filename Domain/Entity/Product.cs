using System;

namespace Domain.Entity
{
    public class Product
    {
        public int Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public decimal Cust { get; private set; }
        public decimal Price { get; private set; }
        public string Ncm { get; private set; }
        public string Reference { get; private set; }
        public DateTime DateRegister { get; private set; }

        private Product()
        { }

        public Product(int code, string name, decimal cust, decimal price, string ncm, string reference, DateTime dateRegister, int id = 0)
        {
            Id = id;
            Code = code;
            Name = name;
            Cust = cust;
            Price = price;
            Ncm = ncm;
            Reference = reference;
            DateRegister = dateRegister;
        }
    }
}
