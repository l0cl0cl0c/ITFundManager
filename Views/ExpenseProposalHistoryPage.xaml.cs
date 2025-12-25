using ITFundManager.ViewModels;

namespace ITFundManager.Views;

public partial class ExpenseProposalHistoryPage : ContentPage
{
    private readonly ExpenseProposalHistoryViewModel _viewModel;

    public ExpenseProposalHistoryPage()
    {
        InitializeComponent();

        _viewModel = Application.Current!
            .Handler!
            .MauiContext!
            .Services
            .GetRequiredService<ExpenseProposalHistoryViewModel>();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
