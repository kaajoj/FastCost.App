namespace FastCost.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
[QueryProperty(nameof(Amount), nameof(Amount))]
public partial class CostPage : ContentPage
{
    public string ItemId
    {
        set { LoadCost(value); }
    }

    public string Amount { get; set; }


    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        base.OnNavigatedTo(state);
        var shellNavigationState = Shell.Current.CurrentState;
        // var numericalValue = int.Parse(Uri.UnescapeDataString(state.Location.Query)); //.GetQueryParameter("param1")));
        // string textValue = Uri.UnescapeDataString(state.Location.PathAndQuery); //GetQueryParameter("param2"));
        // var numericalValue = parameters. GetValue<decimal>("param1");
        // var numericalValue2 = parameters; // .GetValue<decimal>("param1");
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