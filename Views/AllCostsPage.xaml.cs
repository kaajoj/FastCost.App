using FastCost.Models;

namespace FastCost.Views;

public partial class AllCostsPage : ContentPage
{
    private AllCosts _allCosts;

    public AllCostsPage()
	{
        InitializeComponent();
        BindingContext = new AllCosts();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        ((AllCosts)BindingContext)?.LoadCosts();
        SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum().Result.ToString()).Result}";
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