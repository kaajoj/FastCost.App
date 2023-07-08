using FastCost.Models;
using System.Windows.Input;

namespace FastCost;

public partial class AppShell : Shell
{
    public ICommand ExportCommand { get; private set; }

    public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Views.CostPage), typeof(Views.CostPage));

        ExportCommand = new Command(ExportData);
        BindingContext = this;
    }

    private async void ExportData()
    {
        var allCosts = new AllCosts();
        var costs = await allCosts.LoadCostsBackUp();

        var filePath = Path.Combine(FileSystem.AppDataDirectory, "costsBackUp.csv");

        var lines = new List<string>();
        lines.Add("Id,Value,Description,Date,CategoryId");
        foreach (var cost in costs)
        {
            lines.Add($"{cost.Id},{cost.Value},{cost.Description},{cost.Date},{cost.CategoryId}");
        }

        File.WriteAllLines(filePath, lines);

        await DisplayAlert("Success", $"Costs data exported to file. File path: {filePath}", "OK");
    }
}
