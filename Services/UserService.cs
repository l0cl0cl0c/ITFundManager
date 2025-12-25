using System.Linq;
using ITFundManager.Models;
using SQLite;

namespace ITFundManager.Services;

public class UserService : IUserService
{
    private readonly IAppDatabase _db;
    private SQLiteConnection Conn => _db.Conn;

    public UserService(IAppDatabase db)
    {
        _db = db;
        _db.EnsureInitialized();
    }

    public Task<User?> AuthenticateAsync(string username, string password)
    {
        var u = Conn.Table<User>().FirstOrDefault(x => x.AccountName == username && x.IsActive);
        if (u == null) return Task.FromResult<User?>(null);
        if (PasswordHelper.Verify(password, u.PasswordHash))
            return Task.FromResult<User?>(u);
        return Task.FromResult<User?>(null);
    }

    public Task<int> CreateUserAsync(User user, string password, IEnumerable<string>? roles = null)
    {
        user.PasswordHash = PasswordHelper.HashPassword(password);
        var id = Conn.Insert(user);
        if (roles != null)
        {
            foreach (var rn in roles)
            {
                var r = Conn.Table<Role>().FirstOrDefault(x => x.Name == rn);
                if (r != null)
                {
                    Conn.Insert(new UserRole { UserId = user.Id, RoleId = r.Id });
                }
            }
        }
        return Task.FromResult(id);
    }

    public Task<List<User>> GetAllAsync()
    {
        var list = Conn.Table<User>().OrderByDescending(u => u.CreatedAt).ToList();
        return Task.FromResult(list);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return Task.FromResult(Conn.Find<User>(id));
    }

    public Task<int> UpdateAsync(User user)
    {
        return Task.FromResult(Conn.Update(user));
    }

    public Task<int> DeleteAsync(int id)
    {
        var u = Conn.Find<User>(id);
        if (u == null) return Task.FromResult(0);
        return Task.FromResult(Conn.Delete(u));
    }

    public Task AssignRolesAsync(int userId, IEnumerable<string> roleNames)
    {
        // remove old
        var existing = Conn.Table<UserRole>().Where(ur => ur.UserId == userId).ToList();
        foreach (var e in existing) Conn.Delete(e);

        // add new
        foreach (var rn in roleNames)
        {
            var r = Conn.Table<Role>().FirstOrDefault(x => x.Name == rn);
            if (r != null)
                Conn.Insert(new UserRole { UserId = userId, RoleId = r.Id });
        }

        return Task.CompletedTask;
    }

    public Task<List<Role>> GetRolesAsync()
    {
        var list = Conn.Table<Role>().ToList();
        return Task.FromResult(list);
    }

    public Task<List<Role>> GetRolesForUserAsync(int userId)
    {
        var q = from ur in Conn.Table<UserRole>()
                join r in Conn.Table<Role>() on ur.RoleId equals r.Id
                where ur.UserId == userId
                select r;
        return Task.FromResult(q.ToList());
    }
}
