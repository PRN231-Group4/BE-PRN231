namespace View_Wine.Models
{
    public class UserData
    {
        public string Token { get; set; }
        public AccountRes Account { get; set; }
    }

    public class AccountRes
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; } // Thêm RoleId
        public string Status { get; set; }
        public string Role { get; set; } // Thêm Role nếu bạn cần lưu tên vai trò
    }

    public class ApiResponse
    {
        public bool IsBanned { get; set; }
        public int BannedAccountId { get; set; }
        public int Code { get; set; }
        public string SystemCode { get; set; }
        public string Message { get; set; }
        public UserData Data { get; set; }
    }
}
