using FastCost.DAL;
using FastCost.Services;

namespace FastCost;

public partial class App : Application
{
    internal static CostRepository CostRepository;
    internal static CategoryRepository CategoryRepository;
    internal static AllCostsService AllCostsService;
    private readonly AppDbContext _dbContext;

    public App(AppDbContext dbContext, CostRepository costRepository, CategoryRepository categoryRepository, AllCostsService allCostsService)
	{
        InitializeComponent();

        _dbContext = dbContext;
        InitializeDatabase();

        CostRepository = costRepository;
        CategoryRepository = categoryRepository;
        AllCostsService = allCostsService;
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
        return new Window(new AppShell());
    }
}
