using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Services
{
    public interface IAllCostsService
    {
        public Task<List<Cost>> LoadCostsBackUp();
        public Task LoadCostsByMonth(int month);
        public Task<decimal> GetSum(int month);
        public Task<IEnumerable<IGrouping<Category, CostModel>>> GetCostsByMonthGroupByCategory(int month);
    }
}
