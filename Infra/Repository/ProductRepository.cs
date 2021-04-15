using Domain.Entity;
using Domain.Interface.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConferenciaContext _context;

        public ProductRepository(ConferenciaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetByCode(int code)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.Code.Equals(code));
        }

        public async Task<Product> New(Product product)
        {
            EntityEntry<Product> result = await _context.Product.AddAsync(product);

            if (result is not null)
            {
                await _context.SaveChangesAsync();
                return product;
            }

            return null;
        }

        public async Task<Product> Update(Product product)
        {
            var local = _context.Set<Product>().Local.FirstOrDefault(p => p.Id.Equals(product.Id));

            if (local != null)
                _context.Entry(local).State = EntityState.Detached;

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return product;
        }
    }
}
