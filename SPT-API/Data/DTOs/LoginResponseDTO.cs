namespace SPT_API.Data.DTOs
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = null!;
        public string? studentID { get; set; } = null;// unique user ID in student model 
        public string? token { get; set; } = null;// apparently you need one so the server(API) will be stateless
        // it needs to be a java script web token
    }
}
