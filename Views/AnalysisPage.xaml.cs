using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    private AllCosts _allCosts;

    public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }

    public AnalysisPage()
    {
        InitializeComponent();

        // _allCosts = new AllCosts();
        // BindingContext = _allCosts;
        BindingContext = new AllCosts();

        // BindingContext = this;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;
        await ((AllCosts)BindingContext)?.LoadCostsByMonth(currentMonth);
        // ((AllCosts)BindingContext)?.LoadCosts();
        // var costsByCategory = _allCosts.Costs.GroupBy(cost => cost.Category.Name);
        // IEnumerable<IGrouping<Category, CostModel>>[] costsByCategory = { Task.Run(() => ((AllCosts)BindingContext)?.GetCostsByMonthAndCategory(currentMonth).Result).Result};
        var costsByCategory = await ((AllCosts)BindingContext)?.GetCostsByMonthGroupByCategory(currentMonth);
        // GroupCosts = costsByCategory;
        foreach (var entry in costsByCategory)
        {
            // Display the category name.
            var category = entry.Key;
            await Console.Out.WriteLineAsync(category.Name);
            foreach (var cost in entry)
            {
                // Display the cost information.
                Console.WriteLine($"Cost: {cost.Value}, Description: {cost.Description}, Date: {cost.Date}");
            }
        }
        GroupCosts = costsByCategory;

        BindingContext = this;

        // SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";
    }

    // private async void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    // {
    //     DateTime selectedDate = e.NewDate;
    //     await _allCosts.LoadCostsByMonth(selectedDate.Month);
    //     SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(selectedDate.Month).Result.ToString()).Result}";
    // }
}