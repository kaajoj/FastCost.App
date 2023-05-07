using FastCost.DAL.Entities;
using SQLite;

namespace FastCost.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        SQLiteAsyncConnection Database;

        public CategoryRepository()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(ConfigDb.DatabasePath, ConfigDb.Flags);
            await Database.CreateTableAsync<Category>();
            await InitCategoriesAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await Init();
            return await Database.Table<Category>().ToListAsync();
        }

        // public async Task<List<Category>> GetCategoriesNotDoneAsync()
        // {
        //     await Init();
        //     return await Database.Table<Category>().Where(t => t.Done).ToListAsync();
        //
        //     // SQL queries are also possible
        //     //return await Database.QueryAsync<TodoCategory>("SELECT * FROM [TodoCategory] WHERE [Done] = 0");
        // }

        public async Task<Category> GetCategoryAsync(int id)
        {
            await Init();
            return await Database.Table<Category>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveCategoryAsync(Category category)
        {
            await Init();
            if (category.Id != 0)
                return await Database.UpdateAsync(category);
            else
                return await Database.InsertAsync(category);
        }

        public async Task<int> DeleteCategoryAsync(Category category)
        {
            await Init();
            return await Database.DeleteAsync(category);
        }

        private async Task InitCategoriesAsync()
        {
            var categories = new List<Category>()
            {
                new() { Id = 1, Name = "food" },
                new() { Id = 2, Name = "apartment" },
                new() { Id = 2, Name = "shopping" },
                new() { Id = 2, Name = "transport" },
                new() { Id = 2, Name = "trip" },
                new() { Id = 2, Name = "bank" },
                new() { Id = 2, Name = "company" },
                new() { Id = 2, Name = "other" }
            };

            await Database.InsertAllAsync(categories);
        }
    }
}
