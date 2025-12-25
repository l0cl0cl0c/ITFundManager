using ITFundManager.ViewModels;

namespace ITFundManager.Views;

public partial class ExpenseProposalListPage : ContentPage
{
    private readonly ExpenseProposalListViewModel _viewModel;

    public ExpenseProposalListPage()
    {
        InitializeComponent();

        _viewModel = Application.Current!
            .Handler!
            .MauiContext!
            .Services
            .GetRequiredService<ExpenseProposalListViewModel>();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
