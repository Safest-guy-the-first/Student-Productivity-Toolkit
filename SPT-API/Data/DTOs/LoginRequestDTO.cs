namespace SPT_API.Data.DTOs
{
    public class LoginRequestDTO
    {
        public string? _UserName { get; set; } = null;
        public string _Password { get; set; } = null;
        public string? _Email { get; set; } = null;
    }
}
