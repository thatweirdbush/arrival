﻿using BookingManagementSystem.Activation;
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
using BookingManagementSystem.Views;
using BookingManagementSystem.Views.Administrator;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar { get; set; }

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

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            //services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IDao, MockDao>();
            services.AddSingleton<IFileService, FileService>();

            // Data Services
            // TODO: Change to AddScoped when using a real data service
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IRentalDetailFacade, RentalDetailFacade>();
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

            //services.AddSingleton<IRepository<Amenity>, Repository<Amenity>>();
            //services.AddSingleton<IRepository<BadReport>, Repository<BadReport>>();
            //services.AddSingleton<IRepository<Booking>, Repository<Booking>>();
            //services.AddSingleton<IRepository<DestinationTypeSymbol>, Repository<DestinationTypeSymbol>>();
            //services.AddSingleton<IRepository<FAQ>, Repository<FAQ>>();
            //services.AddSingleton<IRepository<Notification>, Repository<Notification>>();
            //services.AddSingleton<IRepository<Payment>, Repository<Payment>>();
            //services.AddSingleton<IRepository<PropertyPolicy>, Repository<PropertyPolicy>>();
            //services.AddSingleton<IRepository<Property>, Repository<Property>>();
            //services.AddSingleton<IRepository<QnA>, Repository<QnA>>();
            //services.AddSingleton<IRepository<User>, Repository<User>>();
            //services.AddSingleton<IRepository<Review>, Repository<Review>>();
            //services.AddSingleton<IRepository<Voucher>, Repository<Voucher>>();

            // Views and ViewModels
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
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginPage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<SignupPage>();
            services.AddTransient<SignupViewModel>();
            services.AddTransient<RecoverPasswordPage>();
            services.AddTransient<RecoverPasswordViewModel>();


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

        App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);
    }
}
