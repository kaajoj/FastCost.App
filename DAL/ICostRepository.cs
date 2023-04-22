﻿using FastCost.DAL.Entities;

namespace FastCost.DAL
{
    internal interface ICostRepository
    {
        Task<List<Cost>> GetCostsAsync();
        Task<Cost> GetCostAsync(int id);
        Task<int> SaveCostAsync(Cost cost);
        Task<int> DeleteCostAsync(Cost cost);
    }
}
