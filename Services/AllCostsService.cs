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

        public async Task<List<CostModel>> LoadCostsByMonth(int month)
        {
            var results = await App.CostRepository.GetCostsByMonth(month);
            var costs = results.Adapt<List<CostModel>>();

            // workaround with linking category to cost
            // TODO: db tables relation
            var categories = await App.CategoryRepository.GetCategoriesAsync();
            foreach (var cost in costs)
            {
                cost.Category = categories.SingleOrDefault(cat => cat.Id == cost.CategoryId);
                if (cost.Category is null)
                {
                    cost.Category = new Category { Name = "no category" };
                }
            }

            return costs;
        }

        public async Task<decimal> GetSum(int month)
        {
            var results = await App.CostRepository.GetCostsByMonth(month);
            var costs = results.Adapt<List<CostModel>>();

            var costsInCurrentMonth = costs.Sum(c => c.Value);
            return (decimal)costsInCurrentMonth;
        }

        public async Task<IEnumerable<IGrouping<Category, CostModel>>> GetCostsByMonthGroupByCategory(int month)
        {
            var results = await App.CostRepository.GetCostsByMonth(month);
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
