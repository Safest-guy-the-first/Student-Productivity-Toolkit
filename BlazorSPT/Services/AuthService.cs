using SPT_API.Data.DTOs;
using SPT_API.Models;
using SPT_API.Services.StudentServices;
using System.Net.Http.Headers;

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


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/spt/StudentModel/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if (result != null && result.Success && !string.IsNullOrEmpty(result.token))
                {
                    _token = result.token;
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);

                    var currentuser = await _httpClient.GetFromJsonAsync<StudentModel>($"/spt/StudentModel/search1/{loginDto._UserName}");
                    CurrentUser = currentuser;

                }
                return result ?? new LoginResponseDTO { Success = false, Message = "Empty response" };
            }

            return new LoginResponseDTO { Success = false, Message = "UnAuthorised" };
        }


    }
}
