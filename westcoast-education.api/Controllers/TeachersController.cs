using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Data;
using westcoast_education.api.ViewModels;

namespace westcoast_education.api.Controllers;

[ApiController]
[Route("api/v1/teachers")]
public class TeachersController : ControllerBase
{
    private readonly WestcoastEducationContext _context;
    public TeachersController(WestcoastEducationContext context)
    {
        _context = context;
    }

    // Lista lärare
    [HttpGet()]
    public async Task<ActionResult> List()
    {
        var result = await _context.Teachers
        .Select(t => new TeacherListViewModel
        {
            Id = t.Id,
            SocialSecurityNumber = t.SocialSecurityNumber,
            Name = t.Name,
            Email = t.Email,
            Address = t.Address,
            PhoneNumber = t.PhoneNumber
        })
        .ToListAsync();
        return Ok(result);
    }

    // Hämta lärare på id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Teachers
        .Select(t => new TeacherDetailsViewModel
        {
            Id = t.Id,
            SocialSecurityNumber = t.SocialSecurityNumber,
            Name = t.Name,
            Email = t.Email,
            Address = t.Address,
            PhoneNumber = t.PhoneNumber,
            Courses = t.Courses!.Select(c => new CourseListViewModel
            {
                CourseName = c.CourseName,
                CourseNumber = c.CourseNumber
            }).ToList()
        })
        .SingleOrDefaultAsync(c => c.Id == id);

        return Ok(result);
    }

    // Stubbar
    // Hämta lärare på e-postadress
    [HttpGet("email/{email}")]
    public ActionResult GetByEmail(string email)
    {
        return Ok(new { message = $"GetByEmail fungerar {email}" });
    }

    // Lägga till en ny lärare
    [HttpPost()]
    public ActionResult AddTeacher()
    {
        return Created(nameof(GetById), new { message = "AddTeacher fungerar" });
    }

    // Uppdatera en lärare
    [HttpPut("{id}")]
    public ActionResult UpdateTeacher(int id)
    {
        return NoContent();
    }

    // Lista vilka kurser som en lärare undervisar
    [HttpGet("{id}/courses")]
    public ActionResult ListCoursesTeaching(int id)
    {
        return Ok(new { message = $"Lista lärarens kurser fungera {id}" });
    }

    // Lägga till kurs som lärare kan undervisa i
    [HttpPatch("{id}/addcourse/")]
    public ActionResult AddCourseToTeacher(int id)
    {
        return NoContent();
    }
}