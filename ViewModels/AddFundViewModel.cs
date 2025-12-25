using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Models;
using ITFundManager.Services;

namespace ITFundManager.ViewModels;

public partial class AddFundViewModel : ObservableObject
{
    private readonly IFundService _fundService;

    private int _fundId = 0;

    // ======================
    // TITLE
    // ======================
    private string _title = "Thêm quỹ";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    // ======================
    // FORM FIELDS
    // ======================
    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string type = string.Empty;
    [ObservableProperty] private decimal balance;
    [ObservableProperty] private string note = string.Empty;

    // ======================
    // CONSTRUCTOR
    // ======================
    public AddFundViewModel(IFundService fundService)
    {
        _fundService = fundService;
    }

    // ======================
    // LOAD FOR EDIT (GỌI TỪ PAGE)
    // ======================
    public void LoadForEdit(Fund fund)
    {
        if (fund == null) return;

        _fundId = fund.Id;
        Title = "Sửa quỹ";

        Name = fund.Name;
        Type = fund.Type;
        Balance = fund.Balance;
        Note = fund.Note;
    }

    // ======================
    // SAVE COMMAND
    // ======================
    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Application.Current!
                .MainPage!
                .DisplayAlert("Lỗi", "Tên quỹ không được trống", "OK");
            return;
        }

        if (_fundId == 0)
        {
            // ➕ THÊM MỚI
            await _fundService.AddAsync(new Fund
            {
                Name = Name,
                Type = Type,
                Balance = Balance,
                Note = Note
            });
        }
        else
        {
            // ✏️ CẬP NHẬT
            await _fundService.UpdateAsync(new Fund
            {
                Id = _fundId,
                Name = Name,
                Type = Type,
                Balance = Balance,
                Note = Note
            });
        }

        // ⬅️ QUAY LẠI DANH SÁCH
        await Shell.Current.GoToAsync("//fundlist");
    }
}
