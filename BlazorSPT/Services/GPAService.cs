using Microsoft.AspNetCore.Http.HttpResults;
using SPT_API.Models;

namespace BlazorSPT.Services
{
    public class GPAService
    {
        public StudentModel? currentUser { private get; set; }
        private readonly HttpClient _httpClient;

        public GPAService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<double> CalculateGPA()
        {
            double GPA;
            var GPArequest = await _httpClient.GetAsync("/spt/g/calculategpa");
            if (GPArequest.IsSuccessStatusCode) 
            {
                string content = await GPArequest.Content.ReadAsStringAsync();
                if(double.TryParse(content, out GPA))
                {
                    return GPA;
                }
                else 
                {
                    throw new Exception("Failed to Parse GPA double");
                }
            }
            else
            {    
                Console.WriteLine(GPArequest.StatusCode);
                throw new Exception($"Http request failed with error {GPArequest.StatusCode}");
            }
        }
    }
}
