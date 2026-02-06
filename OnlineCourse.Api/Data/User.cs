using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Kurs_Platformu.Data;

public class User: BaseEntity
{
    public string EMailAddress { get; set; }
    public string PasswordHash { get; set; }
    public List<UserCourseRegistration> CourseRegistrations { get; set; }
    public List<Course> CoursesGiving { get; set; } // courses user is giving as an Instructor

    [NotMapped]
    public IEnumerable<Course> CoursesTaking => CourseRegistrations.Select(registration => registration.Course); // courses user is taking as a Student

    [NotMapped]
    public string FullName => GetFullName();

    protected virtual string GetFullName()
    {
        return EMailAddress;
    }
}
