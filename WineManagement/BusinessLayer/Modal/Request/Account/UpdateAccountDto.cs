namespace BusinessLayer.Modal.Request.Account
{
    public class UpdateAccountDto
    {
        public int RoleId { get; set; }
        public string Username { get; set; } = null!;
        public string Status { get; set; } = null;

    }
}
