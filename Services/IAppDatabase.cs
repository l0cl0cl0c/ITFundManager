using SQLite;

namespace ITFundManager.Services;

public interface IAppDatabase
{
    SQLiteConnection Conn { get; }
    void EnsureInitialized();
}
