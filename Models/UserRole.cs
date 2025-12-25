using SQLite;

namespace ITFundManager.Models;

public class UserRole
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
