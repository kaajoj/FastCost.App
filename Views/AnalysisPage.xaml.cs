using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    //[ObservableProperty]
    //public string SelectedDate { get; set; }

    //public DateTime selectedDate = DateTime.Now;
    //public DateTime SelectedDate
    //{
    //    get { return selectedDate; }
    //    set
    //    {
    //        if (selectedDate != value)
    //        {
    //            selectedDate = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

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
        //GroupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(currentMonth);
        //((AllCosts)BindingContext)?.GroupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(currentMonth);
        var groupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(currentMonth);
        var test = ((AllCosts)BindingContext)?.GroupCosts;
        test = groupCosts;
        foreach (var costGroup in groupCosts)
        {
            decimal? sum = decimal.Zero;
            foreach (var cost in costGroup)
            {
                sum += cost.Value;
            }
            costGroup.Key.SumValue = sum;
            //((AllCosts)BindingContext)?.GroupCosts.Add(costGroup);
        }

        //var sum = await App.AllCostsService.GetSum(currentMonth);
        SumText.Text = $"Total sum:  {Task.Run(() => App.AllCostsService.GetSum(currentMonth).Result.ToString()).Result}";

        BindingContext = this;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;

        ResetView();

        //GroupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(selectedDate.Month);
        var groupCosts = await App.AllCostsService?.GetCostsByMonthGroupByCategory(selectedDate.Month);
        var test = ((AllCosts)BindingContext)?.GroupCosts;
        test = groupCosts;
        foreach (var costGroup in groupCosts)
        {
            decimal? sum = decimal.Zero;
            foreach (var cost in costGroup)
            {
                sum += cost.Value;
            }
            costGroup.Key.SumValue = sum;
            //((AllCosts)BindingContext)?.GroupCosts.Add(costGroup);

        }

        // var sum = await ((AllCosts)BindingContext)?.GetSum(selectedDate.Month);
        // SumText.Text = $"Total sum: {sum}";

        BindingContext = this;
    }
}