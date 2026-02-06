using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Kurs_Platformu.Data;

public class Course: BaseEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public Video Video { get; set; }
    public string Descripition { get; set; }


    public User Instructor { get; set; } // instructor of this course
    public List<UserCourseRegistration> UserRegistrations { get; set; }

    [NotMapped]
    public IEnumerable<User> RegisteredUsers => UserRegistrations.Select(registration => registration.User); // registered users of this course
}
