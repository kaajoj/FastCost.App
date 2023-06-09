using System.Collections.ObjectModel;
using FastCost.DAL.Entities;
using Mapster;

namespace FastCost.Models
{
    public class AllCosts
    {
        public ObservableCollection<CostModel> Costs { get; set; } = new();
        // public ObservableCollection<IGrouping<Category, CostModel>> GroupCosts { get; set; }
        public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }

        public decimal Sum { get; set; }

        // public AllCosts()
        // {
        // }

        public async Task LoadCosts()
        {
            Costs.Clear();

            var results = await App.CostRepository.GetCostsAsync();
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

            foreach (CostModel cost in costs.OrderBy(cost => cost.Date)) 
                Costs.Add(cost);
        }

        public async Task LoadCostsByMonth(int month)
        {
            Costs.Clear();

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

            foreach (CostModel cost in costs.OrderBy(cost => cost.Date)) 
                Costs.Add(cost);
        }

        public async Task<decimal> GetSum(int month)
        {
            Sum = 0;

            // var currentMonth = DateTime.UtcNow.Date.Month;
            var results = await App.CostRepository.GetCostsByMonth(month);
            var costs = results.Adapt<List<CostModel>>();

            var costsInCurrentMonth = costs.Sum(c => c.Value);
            // return (decimal)costsInCurrentMonth;
            Sum = (decimal)costsInCurrentMonth;
            return Sum;
        }

        public async Task<IEnumerable<IGrouping<Category, CostModel>>> GetCostsByMonthGroupByCategory(int month)
        {
            // var currentMonth = DateTime.UtcNow.Date.Month;
            var results = await App.CostRepository.GetCostsByMonth(month);
            var costs = results.Adapt<List<CostModel>>();

            IEnumerable<IGrouping<Category, CostModel>> costsByCategory = null;
            costsByCategory = costs.GroupBy(cost => cost.Category);

            var categories = await App.CategoryRepository.GetCategoriesAsync();
            foreach (var cost in costs)
            {
                cost.Category = categories.SingleOrDefault(cat => cat.Id == cost.CategoryId);
                if (cost.Category is null)
                {
                    cost.Category = new Category { Name = "no category" };
                }
            }

            GroupCosts = costsByCategory;
            // GroupCosts = new ObservableCollection<IGrouping<Category, CostModel>>(costsByCategory);
            // GroupCosts.Add((IGrouping<Category, CostModel>)costsByCategory);// = new ObservableCollection<IGrouping<Category, CostModel>>(costsByCategory);
            // GroupCosts.Add(costsByCategory);// = new ObservableCollection<IGrouping<Category, CostModel>>(costsByCategory);

            foreach (var costGroup in GroupCosts)
            {
                decimal? sum = decimal.Zero;
                foreach (var cost in costGroup)
                {
                    sum += cost.Value;
                }
                costGroup.Key.SumValue = sum;
            }

            return GroupCosts;
        }
    }
}
