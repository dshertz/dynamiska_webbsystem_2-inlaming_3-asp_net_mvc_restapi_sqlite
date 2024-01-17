using westcoast_education.api.Models;

namespace westcoast_education.api.ViewModels;

public class CourseDetailsViewModel
{
    public int Id { get; set; }
    public string? CourseName { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseNumber { get; set; }
    public DateTime StartDate { get; set; }
    public int LengthInWeeks { get; set; }
    public string? Teacher { get; set; }

    public ICollection<StudentDetailsViewModel>? Students { get; set; }
}