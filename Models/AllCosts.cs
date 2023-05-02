using System.Collections.ObjectModel;
using Mapster;

namespace FastCost.Models
{
    public class AllCosts
    {
        public ObservableCollection<CostModel> Costs { get; set; } = new();

        public AllCosts()
        {
        }

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
            }

            foreach (CostModel cost in costs.OrderBy(cost => cost.Date)) 
                Costs.Add(cost);
        }
    }
}
