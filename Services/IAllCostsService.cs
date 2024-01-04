using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Services
{
    public interface IAllCostsService
    {
        public Task<List<Cost>> LoadCostsBackUp();
        public Task<List<CostModel>> LoadCostsByMonth(DateTime date);
        public Task<decimal> GetSum(DateTime date);
        public Task<IEnumerable<IGrouping<Category, CostModel>>> GetCostsByMonthGroupByCategory(DateTime date);
    }
}
