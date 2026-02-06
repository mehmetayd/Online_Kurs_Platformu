namespace Online_Kurs_Platformu.Data;

public class UserCourseRegistration: BaseEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }

    public User User { get; set; }
    public Course Course { get; set; }
}