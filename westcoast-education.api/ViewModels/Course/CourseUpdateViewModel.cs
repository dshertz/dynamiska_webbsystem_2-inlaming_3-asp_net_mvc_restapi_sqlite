using System.ComponentModel.DataAnnotations;

namespace westcoast_education.api.ViewModels;

public class CourseUpdateViewModel
{
    [Required(ErrorMessage = "Kursnamn saknas")]
    public string? CourseName { get; set; }

    [Required(ErrorMessage = "Kurstitel saknas")]
    public string? CourseTitle { get; set; }

    [Required(ErrorMessage = "Kursnummer saknas")]
    public string? CourseNumber { get; set; }

    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Antal veckor saknas")]
    [Range(1, 20)]
    public int LengthInWeeks { get; set; }
    public string? Teacher { get; set; }

}