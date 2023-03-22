namespace FastCost.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class CostPage : ContentPage
{
    public string ItemId
    {
        set { LoadCost(value); }
    }

    public CostPage()
	{   
		InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.costs.txt";

        LoadCost(Path.Combine(appDataPath, randomFileName));
    }
    private void LoadCost(string fileName)
    {
        var costModel = new Models.Cost
        {
            FileName = fileName
        };

        if (File.Exists(fileName))
        {
            costModel.Date = File.GetCreationTime(fileName);
            costModel.Description = File.ReadAllText(fileName);
        }

        BindingContext = costModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Cost cost)
            File.WriteAllText(cost.FileName, DescriptionEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Cost cost)
        {
            // Delete the file.
            if (File.Exists(cost.FileName))
                File.Delete(cost.FileName);
        }

        await Shell.Current.GoToAsync("..");
    }
}