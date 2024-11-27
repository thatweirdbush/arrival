using BookingManagementSystem.Activation;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Facades;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using BookingManagementSystem.Core.Services;
using BookingManagementSystem.Helpers;
using BookingManagementSystem.Models;
using BookingManagementSystem.Notifications;
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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using WinUIEx.Messaging;

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
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();
            services.AddTransient<GeographicNameService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IDao, MockDao>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<PropertyImagesActivationHandler>();

            // Bussiness Services
            services.AddSingleton<IPropertyService, PropertyService>();
            services.AddTransient<IRoomService, RoomService>();

            // Data Services
            // TODO: Change to AddScoped when using a real data service
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IRentalDetailFacade, RentalDetailFacade>();
            services.AddSingleton<IPaymentFacade, PaymentFacade>();
            services.AddSingleton<IRepository<Amenity>, AmenityRepository>();
            services.AddSingleton<IRepository<BadReport>, BadReportRepository>();
            services.AddSingleton<IRepository<Booking>, BookingRepository>();
            services.AddSingleton<IRepository<DestinationTypeSymbol>, DestinationTypeSymbolRepository>();
            services.AddSingleton<IRepository<FAQ>, FAQRepository>();
            services.AddSingleton<IRepository<Notification>, NotificationRepository>();
            services.AddSingleton<IRepository<Payment>, PaymentRepository>();
            services.AddSingleton<IRepository<PropertyPolicy>, PropertyPolicyRepository>();
            services.AddSingleton<IRepository<Property>, PropertyRepository>();
            services.AddSingleton<IRepository<QnA>, QnARepository>();
            services.AddSingleton<IRepository<User>, UserRepository>();
            services.AddSingleton<IRepository<Review>, ReviewRepository>();
            services.AddSingleton<IRepository<Voucher>, VoucherRepository>();
            services.AddSingleton<IRepository<PropertyTypeIcon>, PropertyTypeIconRepository>();

            // Views and ViewModels
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

        // Run heavy load functions on background threads
        await Task.Run(async () =>
        {
            await RunLongRunningTasksAsync();
        });

        // Officially activate the app when all initialization is done
        await App.GetService<IActivationService>().ActivateAsync(args);
    }

    private async Task RunLongRunningTasksAsync()
    {
        await PropertyImagesActivationHandler.CopyPropertyImagesToLocalFolderAsync();
        await PropertyImagesActivationHandler.BindingPropertyImagesWithLocalFolderAsync();

        // Create notification channel
        App.GetService<IAppNotificationService>().ShowNotification(
            title: "Welcome back to Arrival!",
            message: "Today is a perfect day to start a journey.",
            imageUri: "https://letsenhance.io/static/a31ab775f44858f1d1b80ee51738f4f3/11499/EnhanceAfter.jpg",
            buttons:
            [
                ("Mark as read", "action=mark-as-read"),
                ("See details", "action=see-details")
            ]
        );
    }
}
