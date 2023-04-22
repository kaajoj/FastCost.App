using System.Collections.ObjectModel;
using FastCost.DAL;

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

            var costs = await App.CostRepository.GetCostsAsync();

            foreach (CostModel cost in costs.OrderBy(cost => cost.Date)) 
                Costs.Add(cost);
        }
    }
}
