using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{

    public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }

    private string _selectedDate;
    public string SelectedDate
    {
        get
        {
            return _selectedDate ?? DateTime.Now.ToString();
        }
        set
        {
            _selectedDate = value;
        }
    }

    public AnalysisPage()
    {
        InitializeComponent();
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

        ResetView();
        var currentMonth = DateTime.UtcNow.Date.Month;
        GroupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(currentMonth);
        foreach (var costGroup in GroupCosts)
        {
            decimal? sum = decimal.Zero;
            foreach (var cost in costGroup)
            {
                sum += cost.Value;
            }
            costGroup.Key.SumValue = sum;
        }

        //var sum = await App.AllCostsService.GetSum(currentMonth);
        SumText.Text = $"Total sum:  {Task.Run(() => App.AllCostsService.GetSum(currentMonth).Result.ToString()).Result}";

        BindingContext = this;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;

        //ResetView();
        GroupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(selectedDate.Month);
        foreach (var costGroup in GroupCosts)
        {
            decimal? sumGroup = decimal.Zero;
            foreach (var cost in costGroup)
            {
                sumGroup += cost.Value;
            }
            costGroup.Key.SumValue = sumGroup;

        }

        //var sum = await App.AllCostsService.GetSum(selectedDate.Month);
        // SumText.Text = $"Total sum: {sum}";

        BindingContext = this;
    }
}