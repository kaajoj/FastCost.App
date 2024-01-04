using FastCost.DAL.Entities;
using Mapster;
using SQLite;

namespace FastCost.DAL
{
    public class CostRepository : ICostRepository
    {
        SQLiteAsyncConnection Database;

        public CostRepository()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(ConfigDb.DatabasePath, ConfigDb.Flags);
            await Database.CreateTableAsync<Cost>();
        }

        public async Task<List<Cost>> GetCostsAsync()
        {
            await Init();
            return await Database.Table<Cost>().ToListAsync();
        }

        public async Task<List<Cost>> GetCostsByMonth(DateTime date)
        {
            await Init();

            // var targetDate = new DateTime(DateTime.Now.Year, month, 1);
            // var targetTicks = targetDate.Ticks;

            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1);

            return await Database.Table<Cost>().Where(c => c.Date >= startDate && c.Date < endDate).ToListAsync();
        }

        // public async Task<List<Cost>> GetCostsNotDoneAsync()
        // {
        //     await Init();
        //     return await Database.Table<Cost>().Where(t => t.Done).ToListAsync();
        //
        //     // SQL queries are also possible
        //     //return await Database.QueryAsync<TodoCost>("SELECT * FROM [TodoCost] WHERE [Done] = 0");
        // }

        public async Task<Cost> GetCostAsync(int id)
        {
            await Init();
            return await Database.Table<Cost>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveCostAsync(Cost cost)
        {
            await Init();
            if (cost.Id != 0)
                return await Database.UpdateAsync(cost);
            else
                return await Database.InsertAsync(cost);
        }

        public async Task<int> DeleteCostAsync(Cost cost)
        {
            await Init();
            return await Database.DeleteAsync(cost);
        }
    }
}
