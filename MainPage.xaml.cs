namespace FastCost;

public partial class MainPage : ContentPage
{
	int allCosts = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCostClicked(object sender, EventArgs e)
	{
        allCosts++;

        CostBtn.Text = $"Cost {allCosts} added";

        SemanticScreenReader.Announce(CostBtn.Text);
	}
}

