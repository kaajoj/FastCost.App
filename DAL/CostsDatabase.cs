using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastCost.DAL.Entities;
using SQLite;

namespace FastCost.DAL
{
    public class CostsDatabase
    {
        SQLiteAsyncConnection Database;

        public CostsDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(ConfigDb.DatabasePath, ConfigDb.Flags);
            var result = await Database.CreateTableAsync<Cost>();
        }
        
    }
}
