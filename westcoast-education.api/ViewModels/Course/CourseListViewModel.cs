namespace westcoast_education.api.ViewModels;

public class CourseListViewModel
{
    public int Id { get; set; }
    public string? CourseName { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseNumber { get; set; }
    public DateTime StartDate { get; set; }
    public int LengthInWeeks { get; set; }
    public string? Teacher { get; set; }
}