using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Models;
using ITFundManager.Services;
using System.Collections.ObjectModel;

namespace ITFundManager.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IFundService _fundService;

    public ObservableCollection<Fund> Funds { get; } = new();

    [ObservableProperty]
    private bool isBusy;

    public MainViewModel(IFundService fundService)
    {
        _fundService = fundService;
    }

    // 🔄 LOAD DANH SÁCH
    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        Funds.Clear();

        var list = await _fundService.GetAllAsync();
        foreach (var f in list)
            Funds.Add(f);

        IsBusy = false;
    }

    // 🗑 XÓA QUỸ
    public async Task DeleteFundAsync(int fundId)
    {
        await _fundService.DeleteAsync(fundId);
        await LoadAsync(); // ✅ refresh ngay
    }
}
