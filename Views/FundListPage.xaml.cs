using ITFundManager.Models;
using ITFundManager.ViewModels;

namespace ITFundManager.Views;

public partial class FundListPage : ContentPage
{
    private readonly FundListViewModel _viewModel;

    public FundListPage(FundListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadFundsAsync();
    }

    // ➕ Thêm quỹ
    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//addfund");
    }

    // 📄 Duyệt đề xuất chi
    private async void OnOpenProposalListClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//expenseproposallist");
    }

    // ✏️ Sửa quỹ
    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Fund fund)
        {
            await Shell.Current.GoToAsync("//addfund", new Dictionary<string, object>
            {
                { "Fund", fund }
            });
        }
    }

    // 🗑 Xóa quỹ
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Fund fund)
        {
            bool confirm = await DisplayAlert(
                "Xác nhận",
                $"Xóa quỹ \"{fund.Name}\"?",
                "Xóa",
                "Hủy");

            if (confirm)
            {
                await _viewModel.DeleteFundAsync(fund);
            }
        }
    }
    // 💸 Đề xuất chi
    private async void OnAddProposalClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Fund fund)
        {
            await Shell.Current.GoToAsync(
                "//addexpenseproposal",
                new Dictionary<string, object>
                {
                { "Fund", fund }
                });
        }
    }

}
