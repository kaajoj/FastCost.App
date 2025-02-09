using FastCost.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastCost.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            EnsureCategoriesInitialized().Wait();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategory(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> SaveCategory(Category category)
        {
            if (category.Id != 0)
            {
                _dbContext.Categories.Update(category);
            }
            else
            {
                await _dbContext.Categories.AddAsync(category);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
            return await _dbContext.SaveChangesAsync();
        }
           

        private async Task EnsureCategoriesInitialized()
        {
            if (await _dbContext.Categories.AnyAsync())
                return;

            var categories = new List<Category>
            {
                new() { Id = 1, Name = "food" },
                new() { Id = 2, Name = "apartment" },
                new() { Id = 3, Name = "shopping" },
                new() { Id = 4, Name = "transport" },
                new() { Id = 5, Name = "trip" },
                new() { Id = 6, Name = "bank" },
                new() { Id = 7, Name = "company" },
                new() { Id = 8, Name = "other" }
            };

            await _dbContext.Categories.AddRangeAsync(categories);
            await _dbContext.SaveChangesAsync();
        }
    }
}
