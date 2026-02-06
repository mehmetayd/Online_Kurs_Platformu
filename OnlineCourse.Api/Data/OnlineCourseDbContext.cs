using Microsoft.EntityFrameworkCore;

namespace Online_Kurs_Platformu.Data
{
    public sealed class OnlineCourseDbContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public OnlineCourseDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>(model =>
                {
                    model
                        .HasIndex(user => user.EMailAddress)
                        .IsUnique();

                    model
                        .HasMany(user => user.CourseRegistrations)
                        .WithOne(registration => registration.User);

                    model
                        .HasMany(user => user.CoursesGiving)
                        .WithOne(course => course.Instructor);

                    model.ToTable("tbl_users");
                });

            modelBuilder
                .Entity<Course>(model =>
                {
                    model.HasOne(user => user.Video);

                    model
                        .HasOne(course => course.Instructor)
                        .WithMany(user => user.CoursesGiving);

                    model
                        .HasMany(course => course.UserRegistrations)
                        .WithOne(registration => registration.Course);

                    model.ToTable("tbl_courses");
                });

            modelBuilder
                .Entity<Video>(model =>
                {
                    model
                        .HasOne(video => video.Course)
                        .WithOne(course => course.Video)
                        .HasForeignKey<Video>(video => video.CourseId);

                    model.ToTable("tbl_videos");
                });

            modelBuilder
                .Entity<UserCourseRegistration>(model =>
                {
                    model
                        .HasOne(registration => registration.User)
                        .WithMany(user => user.CourseRegistrations)
                        .HasForeignKey(registration => registration.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                        

                    model
                        .HasOne(registration => registration.Course)
                        .WithMany(course => course.UserRegistrations)
                        .HasForeignKey(registration => registration.CourseId)
                        .OnDelete(DeleteBehavior.Restrict);

                    model
                        .HasIndex(registration => new
                        {
                            registration.UserId,
                            registration.CourseId
                        })
                        .IsUnique();

                    model
                        .HasIndex(registration => new
                        {
                            registration.CourseId,
                            registration.UserId
                        })
                        .IsUnique();

                    model.ToTable("tbl_user_course_registrations");
                });
        }
    }
}
