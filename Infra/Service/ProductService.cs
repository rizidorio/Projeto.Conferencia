using Domain.Dto;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<ProductDto>> ReadDocument(string path)
        {
            throw new NotImplementedException();
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
