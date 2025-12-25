using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Models;
using ITFundManager.Services;
using System.Collections.ObjectModel;

namespace ITFundManager.ViewModels;

public partial class ExpenseProposalListViewModel : ObservableObject
{
    private readonly IExpenseProposalService _service;
    private readonly AppState _appState;

    public ObservableCollection<ExpenseProposal> Proposals { get; } = new();

    [ObservableProperty]
    private bool isBusy;

    public ExpenseProposalListViewModel(
        IExpenseProposalService service,
        AppState appState)
    {
        _service = service;
        _appState = appState;
    }

    // =========================
    // LOAD – CHỈ LẤY CHỜ DUYỆT
    // =========================
    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Proposals.Clear();

            var list = await _service.GetAllAsync();

            foreach (var p in list)
            {
                if (p.Status == ProposalStatus.Pending)
                    Proposals.Add(p);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    // =========================
    // DUYỆT
    // =========================
    [RelayCommand]
    public async Task ApproveAsync(ExpenseProposal proposal)
    {
        if (proposal == null || IsBusy) return;
        if (_appState.CurrentUser == null) return;

        IsBusy = true;
        try
        {
            await _service.ApproveAsync(
                proposal,
                _appState.CurrentUser.AccountName);

            Proposals.Remove(proposal); // 👈 BIẾN MẤT NGAY
        }
        finally
        {
            IsBusy = false;
        }
    }

    // =========================
    // TỪ CHỐI
    // =========================
    [RelayCommand]
    public async Task RejectAsync(ExpenseProposal proposal)
    {
        if (proposal == null || IsBusy) return;
        if (_appState.CurrentUser == null) return;

        IsBusy = true;
        try
        {
            await _service.RejectAsync(
                proposal,
                _appState.CurrentUser.AccountName);

            Proposals.Remove(proposal); // 👈 BIẾN MẤT NGAY
        }
        finally
        {
            IsBusy = false;
        }
    }
}
