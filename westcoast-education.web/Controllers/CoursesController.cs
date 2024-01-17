using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using westcoast_education.web.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace westcoast_education.web.Controllers
{
    [Route("courses")]
    public class CoursesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string? _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public CoursesController(IConfiguration config, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index()
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/courses");

            if (!response.IsSuccessStatusCode) return Content("Ooops det gick fel"); //Errorsida!

            var json = await response.Content.ReadAsStringAsync();

            var courses = JsonSerializer.Deserialize<IList<CourseListViewModel>>(json, _options);

            return View("Index", courses);
        }

        [HttpGet("details/{courseId}")]

        public async Task<IActionResult> Details(int courseId)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/courses/{courseId}");

            if (!response.IsSuccessStatusCode) return Content("Ooops det gick fel"); //Errorsida!

            var json = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<CourseDetailsViewModel>(json, _options);

            return View("Details", course);
        }
        [HttpGet("create")]
        public IActionResult Edit()
        {
            var course = new CoursePostViewModel();
            return View("Create", course);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CoursePostViewModel course)
        {
            if (!ModelState.IsValid) return View("Create", course);

            //vad förväntar sig apiet...
            var model = new
            {
                CourseName = course.CourseName,
                CourseTitle = course.CourseTitle,
                CourseNumber = course.CourseNumber,
                StartDate = course.StartDate,
                LengthInWeeks = course.LengthInWeeks,
                Teacher = course.Teacher
            };

            using var client = _httpClient.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Application.Json);

            var response = await client.PostAsync($"{_baseUrl}/courses", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return Content("Ooops det gick fel"); //Errorsida
        }
    }
}