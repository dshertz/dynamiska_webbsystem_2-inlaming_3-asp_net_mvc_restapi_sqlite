using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Data;
using westcoast_education.api.Models;
using westcoast_education.api.ViewModels;

namespace westcoast_education.api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CoursesController : ControllerBase
{
    private readonly WestcoastEducationContext _context;
    public CoursesController(WestcoastEducationContext context)
    {
        _context = context;
    }

    // Lista kurser
    [HttpGet()]
    public async Task<ActionResult> List()
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseListViewModel
        {
            Id = c.Id,
            CourseName = c.CourseName,
            CourseTitle = c.CourseTitle,
            CourseNumber = c.CourseNumber,
            StartDate = c.StartDate,
            LengthInWeeks = c.LengthInWeeks,
            Teacher = c.Teacher!.Name ?? ""
        })
        .ToListAsync();
        return Ok(result);
    }

    // Hämta kurs på kursid
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseDetailsViewModel
        {
            Id = c.Id,
            CourseName = c.CourseName,
            CourseTitle = c.CourseTitle,
            CourseNumber = c.CourseNumber,
            StartDate = c.StartDate,
            LengthInWeeks = c.LengthInWeeks,
            Teacher = c.Teacher!.Name ?? "",
            Students = c.Students!.Select(s => new StudentDetailsViewModel
            {
                Name = s.Name,
                Email = s.Email
            }).ToList()
        })
        .SingleOrDefaultAsync(c => c.Id == id);

        return Ok(result);
    }

    // Hämta kurs på kursnummer
    [HttpGet("courseno/{courseNo}")]
    public async Task<ActionResult> GetByCourseNumber(string courseNo)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseDetailsViewModel
        {
            Id = c.Id,
            CourseName = c.CourseName,
            CourseTitle = c.CourseTitle,
            CourseNumber = c.CourseNumber,
            LengthInWeeks = c.LengthInWeeks,
            Teacher = c.Teacher!.Name ?? "",
        })
        .SingleOrDefaultAsync(c => c.CourseNumber!.ToUpper().Trim() == courseNo.ToUpper().Trim());

        return Ok(result);
    }

    // Hämta kurs på kurstitel
    [HttpGet("title/{courseTitle}")]
    public ActionResult GetByCourseTitle(string courseTitle)
    {
        return Ok(new { message = $"GetByCourseTitle fungerar {courseTitle}" });
    }

    // Hämta kurs på startdatum
    [HttpGet("startdate/{courseStartDate}")]
    public async Task<ActionResult> GetByCourseStartDate(string courseStartDate)
    {
        var result = await _context.Courses
        .Include(t => t.Teacher)
        .Select(c => new CourseDetailsViewModel
        {
            Id = c.Id,
            CourseName = c.CourseName,
            CourseTitle = c.CourseTitle,
            CourseNumber = c.CourseNumber,
            LengthInWeeks = c.LengthInWeeks,
            Teacher = c.Teacher!.Name ?? "",
        })
        .SingleOrDefaultAsync(c => c.CourseNumber!.Trim() == courseStartDate.Trim());

        return Ok(result);
    }

    // Lägga till en ny kurs
    [HttpPost()]
    public async Task<ActionResult> AddCourse(CourseAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas för att kunna lagra kursen");

        var exists = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber!.ToUpper().Trim() == model.CourseNumber!.ToUpper().Trim());
        if (exists is not null) return BadRequest($"Vi har redan en kurs med kursnummer {model.CourseNumber} i systemet");

        var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Name!.ToUpper().Trim() == model.Teacher!.ToUpper().Trim());
        if (teacher is null) return NotFound($"Läraren {model.Teacher} finns inte i systemet");

        var course = new CourseModel
        {
            CourseName = model.CourseName,
            CourseTitle = model.CourseTitle,
            CourseNumber = model.CourseNumber,
            StartDate = model.StartDate,
            LengthInWeeks = model.LengthInWeeks,
            TeacherId = teacher.Id
        };

        await _context.Courses.AddAsync(course);

        if (await _context.SaveChangesAsync() > 0)
        {
            return Created(nameof(GetById), new { id = course.Id });
        }

        return StatusCode(500, "Internal Server Error");
    }

    // Uppdatera en ny kurs
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, CourseUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("Information saknas för att kunna uppdatera kursen");

        var course = await _context.Courses.FindAsync(id);
        if (course is null) return BadRequest($"Vi kan inte hitta en kurs med kursnummer {model.CourseNumber} i systemet");

        var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Name!.ToUpper().Trim() == model.Teacher!.ToUpper().Trim());
        if (teacher is null) return NotFound($"Läraren {model.Teacher} finns inte i systemet");

        course.CourseName = model.CourseName;
        course.CourseTitle = model.CourseTitle;
        course.CourseNumber = model.CourseNumber;
        course.StartDate = model.StartDate;
        course.LengthInWeeks = model.LengthInWeeks;
        course.TeacherId = teacher.Id;

        _context.Courses.Update(course);

        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }


    // Radera kurs
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course is null) return NotFound($"Vi kan inte hitta någon kurs med id: {id}");

        _context.Courses.Remove(course);

        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

    // Stubbar
    // Markera en kurs som fullbokad
    [HttpPatch("{id}/booking")]
    public ActionResult MarkCourseAsFull(int id)
    {
        return NoContent();
    }

    // Markera en kurs om klar
    [HttpPatch("{id}/status")]
    public ActionResult MarkCourseCompleted(int id)
    {
        return NoContent();
    }
}