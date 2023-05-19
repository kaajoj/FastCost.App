using FastCost.Models;

namespace FastCost.Views;

public partial class AllCostsPage : ContentPage
{
    private AllCosts _allCosts;
    private List<CostModel> _costs;

    public AllCostsPage()
	{
        InitializeComponent();

        _allCosts = new AllCosts();
        BindingContext = _allCosts;

        ((AllCosts)BindingContext)?.LoadCosts();
        _costs = _allCosts.GetCosts();

        var currentMonth = DateTime.UtcNow.Date.Month;
        var costsInCurrentMonth = _costs.Where(c => c.Date.Month == currentMonth).Sum(c => c.Value);
        SumText.Text = $"{costsInCurrentMonth}";
    }

    protected override void OnAppearing()
    {
        ((AllCosts)BindingContext)?.LoadCosts();
        _costs = _allCosts.GetCosts();

        var currentMonth = DateTime.UtcNow.Date.Month;
        var costsInCurrentMonth = _costs.Where(c => c.Date.Month == currentMonth).Sum(c => c.Value);
        SumText.Text = $"{costsInCurrentMonth}";
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CostPage));
    }

    private async void costsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the cost model
            var cost = (CostModel)e.CurrentSelection[0];

            // Should navigate to "CostPage?ItemId=path\on\device\XYZ.costs.txt"
            await Shell.Current.GoToAsync($"{nameof(CostPage)}?{nameof(CostPage.ItemId)}={cost.Id}");

            // Unselect the UI
            costsCollection.SelectedItem = null;
        }
    }
}