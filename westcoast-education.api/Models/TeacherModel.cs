using System.ComponentModel.DataAnnotations;

namespace westcoast_education.api.Models;

public class TeacherModel
{
    [Key]
    public int Id { get; set; }
    public long SocialSecurityNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    // Many-Side
    public ICollection<CourseModel>? Courses { get; set; }
}