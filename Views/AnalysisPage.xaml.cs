using FastCost.DAL.Entities;
using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }
    // public IEnumerable<IGrouping<CategoryModel, CostModel>> GroupCosts2 { get; set; }

    public AnalysisPage()
    {
        InitializeComponent();

        // BindingContext = new AllCosts();

        // BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Wykonaj operacje resetujące stan widoku
        ResetView();
    }

    private void ResetView()
    {
        // Przywróć początkowy stan widoku
        // Wyzeruj zmienne, czyszczenie danych itp.
        BindingContext = new AllCosts();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs state)
    {
        base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;  
        var test = await ((AllCosts)BindingContext)?.GetCostsByMonthGroupByCategory(currentMonth);
        GroupCosts = test;

        // foreach (var costGroup in GroupCosts)
        // {
        //     decimal? sum = decimal.Zero;
        //     foreach (var cost in costGroup)
        //     {
        //         sum += cost.Value;
        //     }
        //     costGroup.Key.SumValue = sum;
        // }

        SumText.Text = $"Total sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";

        BindingContext = this;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        await ((AllCosts)BindingContext)?.GetCostsByMonthGroupByCategory(selectedDate.Month);
        SumText.Text = $"Total sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(selectedDate.Month).Result.ToString()).Result}";
    }
}