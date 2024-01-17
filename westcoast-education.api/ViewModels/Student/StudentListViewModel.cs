namespace westcoast_education.api.ViewModels;

public class StudentListViewModel
{
    public int Id { get; set; }
    public long SocialSecurityNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Course { get; set; }
}