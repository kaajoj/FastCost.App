using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }

    public AnalysisPage()
    {
        InitializeComponent();

        BindingContext = new AllCosts();

        // BindingContext = this;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;
        // IEnumerable<IGrouping<Category, CostModel>>[] costsByCategory = { Task.Run(() => ((AllCosts)BindingContext)?.GetCostsByMonthAndCategory(currentMonth).Result).Result};
        GroupCosts = await ((AllCosts)BindingContext)?.GetCostsByMonthGroupByCategory(currentMonth);

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