
using Microsoft.EntityFrameworkCore;
using RestApi.Interfaces;
using RestApi.Models;

namespace RestApi.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _db;

        public CategoryRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }


        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _db.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.Name = category.Name;

            await _db.SaveChangesAsync();
            return existingCategory;
        }
    }
}