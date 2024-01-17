using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Data;
using westcoast_education.api.ViewModels;

namespace westcoast_education.api.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentsController : ControllerBase
{
    private readonly WestcoastEducationContext _context;
    public StudentsController(WestcoastEducationContext context)
    {
        _context = context;
    }

    // Lista studenter
    [HttpGet()]
    public async Task<ActionResult> List()
    {
        var result = await _context.Students
        .Include(c => c.Course)
        .Select(s => new StudentListViewModel
        {
            Id = s.Id,
            SocialSecurityNumber = s.SocialSecurityNumber,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            Course = s.Course!.CourseName ?? ""
        })
        .ToListAsync();
        return Ok(result);
    }

    // Hämta student på id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Students
        .Include(c => c.Course)
        .Select(s => new StudentDetailsViewModel
        {
            Id = s.Id,
            SocialSecurityNumber = s.SocialSecurityNumber,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            Course = s.Course!.CourseName ?? ""
        })
        .SingleOrDefaultAsync(s => s.Id == id);

        return Ok(result);
    }

    // Hämta student på personnummer
    [HttpGet("socialsecuritynumber/{socialSecurityNumber}")]
    public async Task<ActionResult> GetBySocialSecurityNumber(long socialSecurityNumber)
    {
        var result = await _context.Students
        .Include(c => c.Course)
        .Select(s => new
        {
            Id = s.Id,
            SocialSecurityNumber = s.SocialSecurityNumber,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber
        })
        .SingleOrDefaultAsync(s => s.SocialSecurityNumber == socialSecurityNumber);

        return Ok(result);
    }

    // Hämta student på e-postadress
    [HttpGet("email/{email}")]
    public async Task<ActionResult> GetByEmail(string email)
    {
        var result = await _context.Students
        .Include(c => c.Course)
        .Select(s => new StudentDetailsViewModel
        {
            Id = s.Id,
            SocialSecurityNumber = s.SocialSecurityNumber,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            Course = s.Course!.CourseName ?? ""
        })
        .SingleOrDefaultAsync(s => s.Email!.ToUpper().Trim() == email.ToUpper().Trim());

        return Ok(new { message = $"GetByEmail fungerar {email}" });
    }

    // Stubbar
    // Lägga till ny student
    [HttpPost()]
    public ActionResult AddStudent()
    {
        return Created(nameof(GetById), new { message = "AddStudent fungerar" });
    }

    // Uppdatera en student
    [HttpPut("{id}")]
    public ActionResult UpdateStudent(int id)
    {
        return NoContent();
    }

    // Lista kurser som en student är anmäld på
    [HttpGet("{id}/courses")]
    public ActionResult ListCoursesEnrolled(int id)
    {
        return Ok(new { message = $"Lista anmälda kurser fungerar {id}" });
    }

    // Anmäla en student till nya kurser
    [HttpPatch("{id}/enroll")]
    public ActionResult EnrollStudentToCourse(int id)
    {
        return NoContent();
    }
}
