using System.Security.Cryptography;
using System.Text;

namespace ITFundManager.Services;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return string.Empty;
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool Verify(string password, string hash)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;
        return HashPassword(password) == hash;
    }
}
