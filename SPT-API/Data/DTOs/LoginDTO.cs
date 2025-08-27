namespace SPT_API.Data.DTOs
{
    public class LoginDTO
    {
        public string? _UserName { get; set; } = null;
        public string _Password { get; set; }
        public string? _Email { get; set; } = null;
    }
}
