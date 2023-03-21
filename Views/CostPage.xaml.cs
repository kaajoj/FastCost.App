namespace FastCost.Views;

public partial class CostPage : ContentPage
{
    public CostPage()
	{
		InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.costs.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }
    private void LoadNote(string fileName)
    {
        Models.Cost costModel = new Models.Cost();
        costModel.FileName = fileName;

        if (File.Exists(fileName))
        {
            costModel.Date = File.GetCreationTime(fileName);
            costModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = costModel;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        File.WriteAllText(_fileName, TextEditor.Text);
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (File.Exists(_fileName))
            File.Delete(_fileName);

        TextEditor.Text = string.Empty;
    }
}