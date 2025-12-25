using System.IO;
using Microsoft.Maui.Storage;
using SQLite;
using ITFundManager.Models;

namespace ITFundManager.Services;

public class AppDatabase : IAppDatabase
{
    private readonly SQLiteConnection _conn;
    public SQLiteConnection Conn => _conn;

    public AppDatabase()
    {
        var dbFile = "itfunds.db3";
        var path = Path.Combine(FileSystem.AppDataDirectory, dbFile);
        _conn = new SQLiteConnection(path);

        _conn.CreateTable<Role>();
        _conn.CreateTable<User>();
        _conn.CreateTable<UserRole>();
        _conn.CreateTable<Fund>();
        _conn.CreateTable<ExpenseProposal>();


        EnsureInitialized();
    }

    public void EnsureInitialized()
    {
        if (_conn.Table<Role>().Count() == 0)
        {
            _conn.Insert(new Role { Name = "Admin" });
            _conn.Insert(new Role { Name = "Accountant" });
            _conn.Insert(new Role { Name = "Teacher" });
        }

        if (_conn.Table<User>().Count() == 0)
        {
            var admin = new User
            {
                AccountName = "admin",
                FullName = "Administrator",
                Email = "admin@local",
                PasswordHash = PasswordHelper.HashPassword("admin123"),
                IsActive = true
            };
            _conn.Insert(admin);

            var adminRole = _conn.Table<Role>()
                                 .First(r => r.Name == "Admin");

            _conn.Insert(new UserRole
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            });
        }
    }
}
