using FastCost.DAL.Entities;
using FastCost.Models;
using System.Collections.ObjectModel;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    public ObservableCollection<IGrouping<Category, CostModel>> GroupCosts { get; set; } = new();

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

        var currentDate = DateTime.UtcNow.Date;

        //((AllCostsGroup)BindingContext)?.GroupCosts.Clear();
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
            //((AllCostsGroup)BindingContext)?.GroupCosts.Add(costGroup);
        }

        Sum = await App.AllCostsService.GetSum(currentDate);

        BindingContext = this;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;

        //((AllCostsGroup)BindingContext)?.GroupCosts.Clear();
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
            //((AllCostsGroup)BindingContext)?.GroupCosts.Add(costGroup);
        }

        Sum = await App.AllCostsService.GetSum(selectedDate);

        BindingContext = this;
    }
}