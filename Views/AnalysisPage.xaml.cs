using FastCost.Models;

namespace FastCost.Views;

public partial class AnalysisPage : ContentPage
{
    private AllCosts _allCosts;

    public AnalysisPage()
    {
        InitializeComponent();

        _allCosts = new AllCosts();
        BindingContext = _allCosts;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;
        ((AllCosts)BindingContext)?.LoadCostsByMonth(currentMonth);
        // ((AllCosts)BindingContext)?.LoadCosts();

        // SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";
    }

    // private async void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    // {
    //     DateTime selectedDate = e.NewDate;
    //     await _allCosts.LoadCostsByMonth(selectedDate.Month);
    //     SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(selectedDate.Month).Result.ToString()).Result}";
    // }
}