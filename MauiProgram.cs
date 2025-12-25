using CommunityToolkit.Maui;
using ITFundManager.Services;
using ITFundManager.ViewModels;
using ITFundManager.Views;
using Microsoft.Maui.Hosting;

namespace ITFundManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // DATABASE
        builder.Services.AddSingleton<IAppDatabase, AppDatabase>();

        // SERVICES
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IFundService, FundService>();
        builder.Services.AddSingleton<IExpenseService, ExpenseService>();
        builder.Services.AddSingleton<IExpenseProposalService, ExpenseProposalService>();
        builder.Services.AddSingleton<ExpenseProposalListViewModel>();


        // APP STATE
        builder.Services.AddSingleton<AppState>();
        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<FundListPage>();

        // VIEWMODELS
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<AddFundViewModel>();
        builder.Services.AddTransient<AddExpenseProposalViewModel>();
        builder.Services.AddTransient<ExpenseProposalListViewModel>();
        builder.Services.AddTransient<UserListViewModel>();
        builder.Services.AddTransient<AddEditUserViewModel>();
        builder.Services.AddTransient<FundListViewModel>();


        // PAGES
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<FundListPage>();
        builder.Services.AddTransient<AddFundPage>();
        builder.Services.AddTransient<ExpenseProposalListPage>();
        builder.Services.AddTransient<AddExpenseProposalPage>();
        builder.Services.AddTransient<UsersPage>();
        builder.Services.AddTransient<AddEditUserPage>();

        return builder.Build();
    }
}
