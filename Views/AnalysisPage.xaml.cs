using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }

    public AnalysisPage()
    {
        InitializeComponent();

        // BindingContext = new AllCosts();
        // BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    
        ResetView();
    }

    private void ResetView()
    {
        BindingContext = new AllCosts();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs state)
    {
        base.OnNavigatedTo(state);

        // ResetView();

        var currentMonth = DateTime.UtcNow.Date.Month;
        GroupCosts = await ((AllCosts)BindingContext)?.GetCostsByMonthGroupByCategory(currentMonth);

        SumText.Text = $"Total sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";

        BindingContext = this;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;

        // ResetView();
        BindingContext = new AllCosts();
        GroupCosts = await ((AllCosts)BindingContext)?.GetCostsByMonthGroupByCategory(selectedDate.Month);

        var sum = await ((AllCosts)BindingContext)?.GetSum(selectedDate.Month);
        SumText.Text = $"Total sum: {sum}";

        BindingContext = this;
    }
}