using Microsoft.AspNetCore.Http.HttpResults;
using SPT_API.Data.DTOs;
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

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDto)
        {
            LoginResponseDTO loginRes = new LoginResponseDTO();
            var response = await _httpClient.PostAsJsonAsync("/spt/StudentModel/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                if(result.Result.Success == true && !string.IsNullOrEmpty(result.Result.token))
                {
                    _token = result.Result.token;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_token);
                }
                return await result;    
            }
            loginRes.Success = false;
            loginRes.Message = "UnAuthorised";
            return loginRes;
        }

    }
}
