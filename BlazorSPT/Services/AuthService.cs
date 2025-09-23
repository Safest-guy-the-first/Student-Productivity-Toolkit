using SPT_API.Data.DTOs;
using SPT_API.Models;
using System.Net.Http.Headers;
using System.Net;

namespace BlazorSPT.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private string? _token;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public StudentModel? CurrentUser { get; private set; }
        private readonly Dictionary<string, StudentModel> _activeUsers = new();

        public async void Logout(string userId)
        {
            if (_activeUsers.ContainsKey(userId)) 
            {
                _activeUsers.Remove(userId);
                CurrentUser = null;
            }
        }


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/spt/StudentModel/login", loginDto);
            Console.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                Console.WriteLine(result.Success);
                if (result != null && result.Success && !string.IsNullOrEmpty(result.token))
                {
                    _token = result.token;
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                    var currentuser = new StudentModel();
                    if (loginDto._Email == null) {  currentuser = await _httpClient.GetFromJsonAsync<StudentModel>($"/spt/StudentModel/search1/{loginDto._UserName}"); }
                    else if (loginDto._UserName == null) { currentuser = await _httpClient.GetFromJsonAsync<StudentModel>($"/spt/StudentModel/search2/{loginDto._Email}"); }   
                    CurrentUser = currentuser;
                    _activeUsers[currentuser.uniqueUserId] = currentuser;

                }
                return result ?? new LoginResponseDTO { Success = false, Message = "Empty response" };
            }else if (response.StatusCode == HttpStatusCode.NotFound)
            {

            }

                return new LoginResponseDTO { Success = false, Message = "Login Failed, Username or Password is Incorrect" };
        }
        public async Task<StudentModel> SignUp(StudentModel signUpreq)
        {
            StudentModel createdStudent = new();
            var response = await _httpClient.PostAsJsonAsync("/spt/StudentModel/create", signUpreq);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<StudentModel>();
                createdStudent = result;
            }
            return createdStudent;
        }

    }
}
