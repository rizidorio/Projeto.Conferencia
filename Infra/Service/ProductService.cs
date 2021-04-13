using Domain.Dto;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            try
            {
                IEnumerable<Product> products = await _repository.GetAll();

                if (products.Any())
                {
                    return products.Select(product => new ProductDto(product.Code, product.Name, product.Cust, product.Price, product.Ncm, product.Reference, product.DateRegister));
                }
                else
                {
                    throw new Exception("Não existe produtos cadastrados.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> GetByCode(int code)
        {
            try
            {
                IEnumerable<ProductDto> products = await GetAll();

                return products.FirstOrDefault(p => p.Code.Equals(code));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductDto>> ReadDocument(string path)
        {
            List<ProductDto> products = new List<ProductDto>();
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new Exception("Caminho do arquivo inválido.");
                }

                string[] result = File.ReadAllLines(path);

                if (!result.Any())
                {
                    throw new Exception("Arquivo sem dados.");
                }

                List<string[]> allLines = result.Select(i => i.Split('|')).ToList();

                foreach (string[] line in allLines.Skip(1))
                {
                    int code = int.Parse(line[0].Trim());
                    string name = line[1].Trim();
                    decimal cust = decimal.TryParse(line[2], NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out decimal custValue) ? custValue : 0.00m;
                    decimal price = decimal.TryParse(line[3], NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out decimal priceValue) ? priceValue : 0.00m;
                    string ncm = line[4].Trim();
                    string reference = line[5].Trim();
                    DateTime dateRegister = DateTime.Parse(line[6].Trim());

                    ProductDto product = new ProductDto(code, name, cust, price, ncm, reference, dateRegister);

                    ProductDto productSaved = await Save(product);

                    products.Add(productSaved);
                }

                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> Save(ProductDto productDto)
        {
            try
            {
                Product product = await _repository.GetByCode(productDto.Code);
                Product result;
                Product productConverted;

                if (product is null)
                {
                    productConverted = new Product(productDto.Code, productDto.Name, productDto.Cust, productDto.Price, productDto.Ncm, productDto.Reference, productDto.DateRegister);

                    result = await _repository.New(productConverted);

                    if (result is null)
                    {
                        throw new Exception("Erro o adicionar o produto.");
                    }
                }
                else
                {
                    productConverted = new Product(productDto.Code, productDto.Name, productDto.Cust, productDto.Price, productDto.Ncm, productDto.Reference, productDto.DateRegister, product.Id);

                    result = await _repository.Update(productConverted);

                    if (result is null)
                    {
                        throw new Exception("Erro o atualizar o produto.");
                    }
                }

                return productDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
