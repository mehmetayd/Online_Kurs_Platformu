namespace Online_Kurs_Platformu.Data;

public class Video : BaseEntity
{
    public string Url { get; set; }
    public TimeSpan Duration { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
}