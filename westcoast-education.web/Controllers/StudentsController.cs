using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using westcoast_education.web.ViewModels;

namespace westcoast_education.web.Controllers
{
    [Route("students")]
    public class StudentsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string? _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public StudentsController(IConfiguration config, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index()
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/students");

            if (!response.IsSuccessStatusCode) return Content("Ooops det gick fel");

            var json = await response.Content.ReadAsStringAsync();

            var students = JsonSerializer.Deserialize<IList<StudentListViewModel>>(json, _options);

            return View("Index", students);
        }

        [HttpGet("details/{studentId}")]

        public async Task<IActionResult> Details(int studentId)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/students/{studentId}");

            if (!response.IsSuccessStatusCode) return Content("Ooops det gick fel"); //Errorsida!

            var json = await response.Content.ReadAsStringAsync();
            var student = JsonSerializer.Deserialize<StudentDetailsViewModel>(json, _options);

            return View("Details", student);
        }
    }
}