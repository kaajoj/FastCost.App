using FastCost.DAL;

namespace FastCost;

public partial class App : Application
{
    internal static CostRepository CostRepository;

    public App(CostRepository costRepository)
	{
        InitializeComponent();

		MainPage = new AppShell();

        CostRepository = costRepository;
    }
}
