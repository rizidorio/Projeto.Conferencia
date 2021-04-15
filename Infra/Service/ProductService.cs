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

        public async Task<ProductDto> Update(ProductDto productDto)
        {
            try
            {
                Product product = await _repository.GetByCode(productDto.Code);

                if (product is null)
                {
                    throw new Exception("Produto não encontrado.");
                }

                product = new Product(product.Id, productDto.Code, productDto.Name, productDto.Cust, productDto.Price, productDto.Ncm, productDto.Reference, productDto.DateRegister);
                await _repository.Update(product);

                return productDto;
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

                List<string[]> allLines = result.Select(i => i.Replace("||", "").Split("|")).ToList();

                foreach (string[] line in allLines.Skip(1))
                {
                    int code = int.Parse(line[0].Trim());
                    string name = line[1].Trim();
                    decimal cust = decimal.TryParse(line[2].Replace('.', ','), NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out decimal custValue) ? custValue : 0.00m;
                    decimal price = decimal.TryParse(line[3].Replace('.', ','), NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out decimal priceValue) ? priceValue : 0.00m;
                    string ncm = line[4].Trim();
                    string reference = line[5].Trim();
                    DateTime dateRegister = DateTime.Parse(line[6].Trim());

                    Product product = await _repository.GetByCode(code);

                    if (product is null)
                    {
                        product = new Product(0, code, name, cust, price, ncm, reference, dateRegister);
                        Product productSaved = await _repository.New(product);
                        ProductDto productDto = new ProductDto(productSaved.Code, productSaved.Name, productSaved.Cust, productSaved.Price, productSaved.Ncm, productSaved.Reference, product.DateRegister);
                        products.Add(productDto);
                    }
                    else
                        throw new Exception($"O produto {code} já está cadastrado.");
                }

                return products.Take(100).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
