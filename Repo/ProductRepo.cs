
using Microsoft.EntityFrameworkCore;
using RestApi.Interfaces;
using RestApi.Models;

namespace RestApi.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _db;

        public ProductRepo(AppDbContext db)
        {
            _db = db;
        }

        public Task<bool> ProductExists(int productId)
        {
            return _db.Products.AnyAsync(p => p.Id == productId);
        }
    }
}