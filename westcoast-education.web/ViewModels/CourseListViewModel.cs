namespace westcoast_education.web.ViewModels;

public class CourseListViewModel
{
    public int Id { get; set; }
    public string? CourseName { get; set; }
    public DateTime StartDate { get; set; }
    public int LengthInWeeks { get; set; }
}