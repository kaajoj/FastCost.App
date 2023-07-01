using FastCost.Models;

namespace FastCost.Views;

public partial class AllCostsPage : ContentPage
{
    public AllCostsPage()
	{
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs state)
    {
        base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;
        await ((AllCosts)BindingContext)?.LoadCostsByMonth(currentMonth);


        var sum = await ((AllCosts)BindingContext)?.GetSum(currentMonth);
        SumText.Text = $"Total sum: {sum}";
        // SumText.Text = $"Total sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CostPage));
    }

    private async void CostsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the cost model
            var cost = (CostModel)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(CostPage)}?{nameof(CostPage.ItemId)}={cost.Id}");

            // Unselect the UI
            costsCollection.SelectedItem = null;
        }
    }

    private async void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        await ((AllCosts)BindingContext)?.LoadCostsByMonth(selectedDate.Month);

        // SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(selectedDate.Month).Result.ToString()).Result}";
        var sum = await ((AllCosts)BindingContext)?.GetSum(selectedDate.Month);
        SumText.Text = $"Total sum: {sum}";
    }

    private async void OnExportClicked(object sender, EventArgs e)
    {
        var costs = await ((AllCosts)BindingContext)?.LoadCostsBackUp();

        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dane.csv");

        var lines = new List<string>();
        lines.Add("Id,Value,Description,Date,CategoryId");
        foreach (var cost in costs)
        {
            lines.Add($"{cost.Id},{cost.Value},{cost.Description},{cost.Date},{cost.CategoryId}");
        }

        if(!File.Exists(filePath))
            File.Create(filePath);
        
        File.WriteAllLines(filePath, lines);

        string readText = File.ReadAllText(filePath);
        Console.WriteLine(readText);

        DisplayAlert("Success", "Costs data exported to file.", "OK");

        ExportBtn.Text = $"Costs data exported to file";

        SemanticScreenReader.Announce(ExportBtn.Text);
    }
}