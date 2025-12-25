using SQLite;
using System;

namespace ITFundManager.Models;

public class Fund
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // ❗ KHÔNG dùng Type trần trong ViewModel
    public string Type { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
