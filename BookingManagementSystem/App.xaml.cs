using BookingManagementSystem.Activation;
using BookingManagementSystem.Contracts.Facades;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using BookingManagementSystem.Core.Services;
using BookingManagementSystem.Facades;
using BookingManagementSystem.Models;
using BookingManagementSystem.Services;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.ViewModels.Administrator;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;
using BookingManagementSystem.ViewModels.Payment;
using BookingManagementSystem.Views;
using BookingManagementSystem.Views.Account;
using BookingManagementSystem.Views.Administrator;
using BookingManagementSystem.Views.Client;
using BookingManagementSystem.Views.Host;
using BookingManagementSystem.Views.Host.CreateListingSteps;
using BookingManagementSystem.Views.Payment;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;

namespace BookingManagementSystem;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; set; } = new MainWindow();

    public static UIElement? AppTitlebar
    {
        get; set;
    }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureAppConfiguration((context, config) =>
        {
            // Integrate User Secrets
            config.AddUserSecrets<App>();
        }).
        ConfigureServices((context, services) =>
        {
            // App Hosted Services
            services.AddSingleton<IConfiguration>(context.Configuration);

            // Add logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddDebug(); // Log to Debug Output
                loggingBuilder.AddConsole(); // Log to Console
            });

            // This allows the application to use the ApplicationDbContext at runtime via DI.
            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();
            services.AddSingleton<IFileService, FileService>();

            // Bussiness Services
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IPropertyService, PropertyService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<GeographicNameService>();

            // Data Services
            // TODO: Change to AddScoped when using a real data service
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IRentalDetailFacade, RentalDetailFacade>();
            services.AddTransient<IPaymentFacade, PaymentFacade>();
            services.AddTransient<IRepository<Amenity>, AmenityRepository>();
            services.AddTransient<IRepository<BadReport>, BadReportRepository>();
            services.AddTransient<IRepository<Booking>, BookingRepository>();
            services.AddTransient<IRepository<CountryInfo>, CountryInfoRepository>();
            services.AddTransient<IRepository<FAQ>, FAQRepository>();
            services.AddTransient<IRepository<Notification>, NotificationRepository>();
            services.AddTransient<IRepository<Payment>, PaymentRepository>();
            services.AddTransient<IRepository<PropertyPolicy>, PropertyPolicyRepository>();
            services.AddTransient<IRepository<Property>, PropertyRepository>();
            services.AddTransient<IRepository<QnA>, QnARepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Review>, ReviewRepository>();
            services.AddTransient<IRepository<Voucher>, VoucherRepository>();
            services.AddTransient<DestinationTypeSymbolRepository>();
            services.AddTransient<PropertyTypeIconRepository>();

            // Views and ViewModels
            services.AddTransient<ChatViewModel>();
            services.AddTransient<ChatPage>();
            services.AddTransient<NotificationViewModel>();
            services.AddTransient<NotificationPage>();
            services.AddTransient<FloorPlanViewModel>();
            services.AddTransient<FloorPlanPage>();
            services.AddTransient<PlaceLocationViewModel>();
            services.AddTransient<PlaceLocationPage>();
            services.AddTransient<PlaceDescriptionViewModel>();
            services.AddTransient<PlaceDescriptionPage>();
            services.AddTransient<FinishSetupViewModel>();
            services.AddTransient<FinishSetupPage>();
            services.AddTransient<PublishCelebrationViewModel>();
            services.AddTransient<PublishCelebrationPage>();
            services.AddTransient<ReviewListingViewModel>();
            services.AddTransient<ReviewListingPage>();
            services.AddTransient<SetPriceViewModel>();
            services.AddTransient<SetPricePage>();
            services.AddTransient<PlacePhotosViewModel>();
            services.AddTransient<PlacePhotosPage>();
            services.AddTransient<AmenitiesViewModel>();
            services.AddTransient<AmenitiesPage>();
            services.AddTransient<StandOutViewModel>();
            services.AddTransient<StandOutPage>();
            services.AddTransient<PlaceStructureViewModel>();
            services.AddTransient<PlaceStructurePage>();
            services.AddTransient<AboutYourPlaceViewModel>();
            services.AddTransient<AboutYourPlacePage>();
            services.AddTransient<CreateListingViewModel>();
            services.AddTransient<CreateListingPage>();
            services.AddTransient<ReservationsViewModel>();
            services.AddTransient<ReservationsPage>();
            services.AddTransient<ListingViewModel>();
            services.AddTransient<ListingPage>();
            services.AddTransient<UserProfileViewModel>();
            services.AddTransient<UserProfilePage>();
            services.AddTransient<LoyaltyProgramViewModel>();
            services.AddTransient<LoyaltyProgramPage>();
            services.AddTransient<BookingHistoryViewModel>();
            services.AddTransient<BookingHistoryPage>();
            services.AddTransient<ListingRequestViewModel>();
            services.AddTransient<ListingRequestPage>();
            services.AddTransient<ReportViewModel>();
            services.AddTransient<ReportPage>();
            services.AddTransient<AdministratorViewModel>();
            services.AddTransient<AdministratorPage>();
            services.AddTransient<PaymentViewModel>();
            services.AddTransient<PaymentPage>();
            services.AddTransient<FAQViewModel>();
            services.AddTransient<FAQPage>();
            services.AddTransient<HostViewModel>();
            services.AddTransient<HostPage>();
            services.AddTransient<MapViewModel>();
            services.AddTransient<MapPage>();
            services.AddTransient<RentalDetailPage>();
            services.AddTransient<RentalDetailViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<HomePage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<SignupPage>();
            services.AddTransient<SignupViewModel>();
            services.AddTransient<RecoverPasswordPage>();
            services.AddTransient<RecoverPasswordViewModel>();
            services.AddTransient<SplashScreenPage>();

            // These page's viewmodels are registered as singletons because they are used in multiple places
            services.AddSingleton<LoginViewModel>();
            services.AddTransient<LoginPage>();
            services.AddTransient<ShellPage>();
            services.AddSingleton<ShellViewModel>();
            services.AddTransient<WishlistViewModel>();
            services.AddSingleton<WishlistPage>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();
        App.GetService<IAppNotificationService>().Initialize();
        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        // Activate Splash Screen
        MainWindow.Content = new SplashScreenPage();
        MainWindow.Activate();

        //Run heavy load functions on background threads
        await Task.Run(async () =>
        {
            await RunLongRunningTasksAsync();
        });

        // Officially activate the app when all initialization is done
        await App.GetService<IActivationService>().ActivateAsync(args);
    }

    private static async Task RunLongRunningTasksAsync()
    {
        await Task.Delay(100);

        // Create notification channel
        App.GetService<IAppNotificationService>().ShowNotification(
            title: "Welcome back to Arrival!",
            message: "Today is a perfect day to start a journey.",
            imageUri: "https://letsenhance.io/static/a31ab775f44858f1d1b80ee51738f4f3/11499/EnhanceAfter.jpg",
            buttons:
            [
                ("See details", "action=see-details"),
                ("Dismiss", "action=dismiss")
            ]
        );
    }
}
