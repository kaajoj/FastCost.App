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
            
            foreach (CostModel cost in costs.OrderBy(cost => cost.Date)) 
                Costs.Add(cost);
        }
    }
}
