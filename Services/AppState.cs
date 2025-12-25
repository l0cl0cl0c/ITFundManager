using ITFundManager.Models;

namespace ITFundManager.Services
{
    public class AppState
    {
        // Người dùng hiện tại
        public User? CurrentUser { get; private set; }

        // Kiểm tra đăng nhập
        public bool IsLoggedIn => CurrentUser is not null;

        // Đăng nhập
        public void Login(User user)
        {
            CurrentUser = user;
        }

        // Đăng xuất
        public void Logout()
        {
            CurrentUser = null;
        }

        // Phân quyền (tạm thời)
        public bool IsAdmin => false;
        public bool IsAccountant => false;
        public bool IsTeacher => false;
    }
}
