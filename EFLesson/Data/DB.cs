using Microsoft.EntityFrameworkCore;

namespace EFLesson.Data
{
    /// <summary>
    /// DB Context
    /// </summary>
    public class DB : DbContext
    {
        /// <summary>
        /// Addresses
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Exams
        /// </summary>
        public DbSet<Exam> Exams { get; set; }

        /// <summary>
        /// Courses
        /// </summary>
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Students
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public DbSet<Address> Address { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        
        /// <summary>
        /// On Model Creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Associative entity table mapping
            

            modelBuilder.Entity<Student>().HasMany(s => s.Courses).WithMany(c => c.Students)
                .UsingEntity<Exam>();

            base.OnModelCreating(modelBuilder);

            

            //SEED

            modelBuilder.Entity<Address>().HasData(new Address()
            {
                Id = 1,
                City = "Rome",
                Street = "Corso Street 1",
            });

            modelBuilder.Entity<Address>().HasData(new Address()
            {
                Id = 2,
                City = "Milan",
                Street = "Main Street 1",
            });

            modelBuilder.Entity<Address>().HasData(new Address()
            {
                Id = 3,
                City = "New York",
                Street = "Brodway 1",
            });

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                BirthDate = new DateTime(1980, 1, 17),
                Name = "John",
                Surname = "Doe",
            });

            modelBuilder.Entity<Student>().HasData(new Student()
            {
                Id = 2,
                BirthDate = new DateTime(1984, 7, 8),
                Name = "Mark",
                Surname = "Twain",
                Code = "M00001"
            });

            modelBuilder.Entity<Student>().HasData(new Student()
            {
                Id = 3,
                BirthDate = new DateTime(1984, 8, 20),
                Name = "Susan",
                Surname = "White",
                Code = "M00002"
            });

            modelBuilder.Entity<Course>().HasData(new Course()
            {
                Id = 1,
                Name = "History",
                Description = "Cover all humankind History"
            });

            modelBuilder.Entity<Course>().HasData(new Course()
            {
                Id = 2,
                Name = "Math",
                Description = "From basic operation to integral"
            });



            modelBuilder.Entity("AddressUser").HasData(new { UsersId = 1, AddressesId = 1 });
            modelBuilder.Entity("AddressUser").HasData(new { UsersId = 1, AddressesId = 2 });
            modelBuilder.Entity("AddressUser").HasData(new { UsersId = 2, AddressesId = 3 });

            modelBuilder.Entity<Exam>().HasData(new Exam() { CourseId = 1, StudentId = 3, Date = null, Vote = null });
            modelBuilder.Entity<Exam>().HasData(new Exam() { CourseId = 1, StudentId = 2, Date = null, Vote = null });
            modelBuilder.Entity<Exam>().HasData(new Exam() { CourseId = 2, StudentId = 2, Date = new DateTime(2024, 2, 2), Vote = 25 });


        }


    }


}
