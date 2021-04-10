using Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface.Service
{
    public interface IProductService
    {
        Task<ProductDto> Save(ProductDto productDto);
        Task<ProductDto> GetByCode(int code);
        Task<IEnumerable<ProductDto>> ReadDocument(string path);
        Task<IEnumerable<ProductDto>> GetAll();
    }
}
