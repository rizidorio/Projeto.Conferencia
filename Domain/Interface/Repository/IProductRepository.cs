using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface.Repository
{
    public interface IProductRepository
    {
        Task<Product> New(Product product);
        Task<Product> Update(Product product);
        Task<Product> GetByCode(int code);
        Task<IEnumerable<Product>> GetAll();
    }
}
