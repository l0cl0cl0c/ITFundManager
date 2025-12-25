using SQLite;

namespace ITFundManager.Models;

public class Role
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Admin, Accountant, Head, User...
}
