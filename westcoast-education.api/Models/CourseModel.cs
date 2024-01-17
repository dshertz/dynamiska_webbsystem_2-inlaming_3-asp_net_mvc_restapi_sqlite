using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_education.api.Models;

public class CourseModel
{
    [Key]
    public int Id { get; set; }
    public string? CourseName { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseNumber { get; set; }
    public DateTime StartDate { get; set; }
    public int LengthInWeeks { get; set; }
    public int? TeacherId { get; set; }

    // The One-Side...
    [ForeignKey("TeacherId")]
    public TeacherModel? Teacher { get; set; }

    // The Many-Side
    public ICollection<StudentModel>? Students { get; set; }
}