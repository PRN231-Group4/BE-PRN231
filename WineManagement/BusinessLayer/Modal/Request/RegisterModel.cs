namespace BusinessLayer.Modal.Request
{
    public class AdminCreateAccountModel
    {
        public string Username { get; set; }
        //public string Password { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }
        public DateTime CreateAt { get; set; } = new DateTime();

    }

    public class RegisterModel : AdminCreateAccountModel
    {
        public string Password { get; set; }


    }
}
