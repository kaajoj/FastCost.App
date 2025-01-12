using FastCost.DAL.Entities;
using FastCost.Models;
using Mapster;

namespace FastCost.Services
{
    public class AllCostsService : IAllCostsService
    {
        public async Task<List<Cost>> LoadCostsBackUp()
        {
            var results = await App.CostRepository.GetCostsAsync();
            return results;
        }

        public async Task<List<Cost>> LoadCostsByMonth(DateTime date)
        {
            return await App.CostRepository.GetCostsByMonth(date);
        }

        public async Task<decimal> GetSum(DateTime date)
        {
            var costs = await App.CostRepository.GetCostsByMonth(date);
            return costs.Sum(c => c.Value);
        }

        public async Task<IEnumerable<IGrouping<CategoryModel, CostModel>>> GetCostsByMonthGroupByCategory(DateTime date)
        {
            var results = await App.CostRepository.GetCostsByMonth(date);
            var costs = results.Adapt<List<CostModel>>();

            return costs.GroupBy(cost => cost.Category);
        }
    }
}
