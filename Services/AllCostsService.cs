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
            var results = await App.CostRepository.GetCostsByMonth(date);
            var costs = results.Adapt<List<CostModel>>();

            var costsInCurrentMonth = costs.Sum(c => c.Value);
            return (decimal)costsInCurrentMonth;
        }

        public async Task<IEnumerable<IGrouping<Category, CostModel>>> GetCostsByMonthGroupByCategory(DateTime date)
        {
            var results = await App.CostRepository.GetCostsByMonth(date);
            var costs = results.Adapt<List<CostModel>>();

            var costsByCategory = costs.GroupBy(cost => cost.Category);

            var categories = await App.CategoryRepository.GetCategoriesAsync();
            foreach (var cost in costs)
            {
                cost.Category = categories.SingleOrDefault(cat => cat.Id == cost.CategoryId);
                if (cost.Category is null)
                {
                    cost.Category = new Category { Name = "no category" };
                }
            }

            return costsByCategory;
        }
    }
}
