using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_education.api.Models;

public class StudentModel
{
    [Key]
    public int Id { get; set; }
    public long SocialSecurityNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public int CourseId { get; set; }

    // The One-Side...
    [ForeignKey("CourseId")]
    public CourseModel? Course { get; set; }
}