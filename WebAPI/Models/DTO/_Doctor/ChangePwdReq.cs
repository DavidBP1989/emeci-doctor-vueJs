namespace WebAPI.Models.DTO._Doctor
{
    public class ChangePwdReq
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; } = "";
    }
}