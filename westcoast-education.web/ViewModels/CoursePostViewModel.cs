using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace westcoast_education.web.ViewModels;

public class CoursePostViewModel
{
    [Required(ErrorMessage = "Kursnamn är obligatiskt")]
    [DisplayName("Kursnamn")]
    public string CourseName { get; set; } = "";

    [Required(ErrorMessage = "Kurstitel är obligatiskt")]
    [DisplayName("Kurstitel")]
    public string CourseTitle { get; set; } = "";

    [Required(ErrorMessage = "Kursnummer är är obligatiskt")]
    [DisplayName("Kursnummer")]
    public string CourseNumber { get; set; } = "";

    [Required(ErrorMessage = "Startdatum är obligatiskt")]
    [DisplayName("Startdatum")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Antal veckor är obligatiskt")]
    [DisplayName("Kurslängd")]
    [Range(1, 20, ErrorMessage = "Kurslängd är obligatoriskt [felaktigt antal veckor]")]
    public int? LengthInWeeks { get; set; }

    [Required(ErrorMessage = "Lärare är obligatiskt")]
    [DisplayName("Lärare")]
    public string Teacher { get; set; } = "";
}