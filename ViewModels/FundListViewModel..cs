using CommunityToolkit.Mvvm.ComponentModel;
using ITFundManager.Models;
using ITFundManager.Services;
using System.Collections.ObjectModel;

namespace ITFundManager.ViewModels;

public partial class FundListViewModel : ObservableObject
{
    private readonly IFundService _fundService;

    public ObservableCollection<Fund> Funds { get; } = new();

    public FundListViewModel(IFundService fundService)
    {
        _fundService = fundService;
    }

    public async Task LoadFundsAsync()
    {
        Funds.Clear();
        var list = await _fundService.GetAllAsync();
        foreach (var fund in list)
            Funds.Add(fund);
    }

    // ✅ THÊM HÀM NÀY
    public async Task DeleteFundAsync(Fund fund)
    {
        await _fundService.DeleteAsync(fund.Id);
        Funds.Remove(fund);
    }
}
