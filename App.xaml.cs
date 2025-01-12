using FastCost.Services;

namespace FastCost;

public partial class App : Application
{
    private readonly IAllCostsService _allCostsService;
    private readonly AppDbContext _dbContext;

    public App(AppDbContext dbContext, IAllCostsService allCostsService)
	{
        InitializeComponent();

        _dbContext = dbContext;
        InitializeDatabase();

        _allCostsService = allCostsService;
    }

    private void InitializeDatabase()
    {
        try
        {
            _dbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating database: {ex.Message}");
        }
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell(_allCostsService));
    }
}
