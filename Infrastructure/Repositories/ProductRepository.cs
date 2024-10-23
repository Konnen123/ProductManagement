﻿using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Guid> AddAsync(Product product)
        {
            if( product != null)
            {
                await applicationDbContext.Products.AddAsync(product);
                await applicationDbContext.SaveChangesAsync();
                return product.Id;
            }
            return Guid.Empty;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await GetAsync(id);
            if (product != null)
            {
                applicationDbContext.Products.Remove(product);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await applicationDbContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            return await applicationDbContext.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            if(product != null)
            {
                applicationDbContext.Products.Update(product);
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
