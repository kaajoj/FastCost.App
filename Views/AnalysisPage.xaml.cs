using FastCost.Models;
using System.Collections.ObjectModel;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    public ObservableCollection<IGrouping<CategoryModel, CostModel>> GroupCosts { get; set; } = new();

    public DateTime selectedDate = DateTime.Now;
    public DateTime SelectedDate
    {
        get { return selectedDate; }
        set
        {
            if (selectedDate != value)
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal sum = 0;
    public decimal Sum
    {
        get { return sum; }
        set
        {
            if (sum != value)
            {
                sum = value;
                OnPropertyChanged();
            }
        }
    }

    public AnalysisPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs state)
    {
        base.OnNavigatedTo(state);

        var currentDate = SelectedDate;

        GroupCosts.Clear();
        var groupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(currentDate);
        foreach (var costGroup in groupCosts)
        {
            decimal? sumGroup = decimal.Zero;
            foreach (var cost in costGroup)
            {
                sumGroup += cost.Value;
            }
            costGroup.Key.SumValue = sumGroup;

            GroupCosts.Add(costGroup);
        }

        Sum = await App.AllCostsService.GetSum(currentDate);

        BindingContext = this;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;

        GroupCosts.Clear();
        var groupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(selectedDate);
        foreach (var costGroup in groupCosts)
        {
            decimal? sumGroup = decimal.Zero;
            foreach (var cost in costGroup)
            {
                sumGroup += cost.Value;
            }
            costGroup.Key.SumValue = sumGroup;

            GroupCosts.Add(costGroup);
        }

        Sum = await App.AllCostsService.GetSum(selectedDate);

        BindingContext = this;
    }
}