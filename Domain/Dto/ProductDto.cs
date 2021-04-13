using System;

namespace Domain.Dto
{
    public class ProductDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public decimal Cust { get; set; }
        public decimal Price { get; set; }
        public string Ncm { get; set; }
        public string Reference { get; set; }
        public DateTime DateRegister { get; set; }

        public ProductDto(int code, string name, decimal cust, decimal price, string ncm, string reference, DateTime dateRegister)
        {
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
