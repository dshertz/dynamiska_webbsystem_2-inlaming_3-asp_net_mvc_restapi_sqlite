using System.Text.Json;
using westcoast_education.api.Models;

namespace westcoast_education.api.Data;

public static class SeedData
{
    public static async Task LoadCourseData(WestcoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Courses.Any()) return;

        var json = System.IO.File.ReadAllText("Data/json/courses.json");
        var courses = JsonSerializer.Deserialize<List<CourseModel>>(json, options);

        if (courses is not null && courses.Count > 0)
        {
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadStudentData(WestcoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Students.Any()) return;

        var json = System.IO.File.ReadAllText("Data/json/students.json");
        var students = JsonSerializer.Deserialize<List<StudentModel>>(json, options);

        if (students is not null && students.Count > 0)
        {
            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadTeacherData(WestcoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Teachers.Any()) return;

        var json = System.IO.File.ReadAllText("Data/json/teachers.json");
        var teachers = JsonSerializer.Deserialize<List<TeacherModel>>(json, options);

        if (teachers is not null && teachers.Count > 0)
        {
            await context.Teachers.AddRangeAsync(teachers);
            await context.SaveChangesAsync();
        }
    }
}