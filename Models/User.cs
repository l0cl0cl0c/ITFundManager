using SQLite;
using System;

namespace ITFundManager.Models;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }


    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty; // username
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
