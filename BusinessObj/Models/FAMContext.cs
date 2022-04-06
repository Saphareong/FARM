using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#nullable disable
namespace BusinessObj.Models
{
    public class FAMContext : DbContext
    {
        public FAMContext() { }
        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationType> ApplicationTypes { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<ExamType> ExamTypes { get; set; }

        public DbSet<FinalExam> FinalExams { get; set; }

        public DbSet<Major> Majors { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserMajor> UserMajors { get; set; }

        public DbSet<Slot> Slots { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomDetail> RoomDetails { get; set; }

        public DbSet<TimeTable> TimeTables { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<UserClass> UserClasses { get; set; }   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FAMConnectionString"));
           /*optionsBuilder.UseSqlServer("Server=(local);Database=FAM;Trusted_Connection=True;MultipleActiveResultSets=true");*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUser(modelBuilder);
            this.SeedApplications(modelBuilder);
            this.SeedMajors(modelBuilder);


            //modelBuilder.Entity<UserMajor>()
            //.HasKey(m => new { m.AccountId, m.MajorCode });

            //modelBuilder.Entity<UserMajor>()
            //    .HasOne(um => um.User)
            //    .WithMany(u => u.UserMajors)
            //    .HasForeignKey(um => um.AccountId);

            //modelBuilder.Entity<UserMajor>()
            //    .HasOne(um => um.Major)
            //    .WithMany(m => m.UserMajors)
            //    .HasForeignKey(um => um.MajorCode);
        }

        private void SeedUser(ModelBuilder builder)
        {

            builder.Entity<User>().HasData(new User()
            {
                AccountId = "AD01",
                AccountName = "Admin",
                Password = "aA@123",
                Ower = "Admin",
                RoleId = "AD",
                Email = "admin@gmail.com",
                Phone = "0398462788",
                Status = "ON"

            });
            builder.Entity<User>().HasData(new User()
            {
                AccountId = "ST01",
                AccountName = "Student",
                Password = "aA@123",
                Ower = "Student Test",
                RoleId = "ST",
                Email = "student@gmail.com",
                Phone = "0398462788",
                Status = "ON"

            });
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role()
            {
                RoleId = "AD",
                RoleName = "Admin"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleId = "ST",
                RoleName = "Student"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleId = "TE",
                RoleName = "Teacher"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleId = "PA",
                RoleName = "Parent"
            });
        }
        private void SeedApplications(ModelBuilder builder)
        {
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT1",
                ApplicationTypeName = "Đề nghị miễn điểm danh"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT2",
                ApplicationTypeName = "Đề nghị cấp bảng điểm quá trình"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT3",
                ApplicationTypeName = "Đề nghị chuyển đổi tín chỉ"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT4",
                ApplicationTypeName = "Đề nghị phúc tra"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT5",
                ApplicationTypeName = "Đăng kí thi cải thiện điểm"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT6",
                ApplicationTypeName = "Đề nghị chuyển ngành"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT7",
                ApplicationTypeName = "Đề nghị chuyển cơ sở"
            });
            builder.Entity<ApplicationType>().HasData(new ApplicationType()
            {
                ApplicationTypeID = "AT8",
                ApplicationTypeName = "Đề nghị rút tiền"
            });
        }

        private void SeedMajors(ModelBuilder builder)
        {
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "SE",
                MajorName = "Software Engineer"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "AI",
                MajorName = "Artificial Intelligence"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "MC",
                MajorName = "Multimedia Communication"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "IB",
                MajorName = "International Business"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "GD",
                MajorName = "Graphic Design"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "EL",
                MajorName = "English Language"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "JL",
                MajorName = "Japanese Language"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "IA",
                MajorName = "Information Assurance"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "HM", //hitman
                MajorName = "Hospitality Management"
            });
            builder.Entity<Major>().HasData(new Major()
            {
                MajorCode = "DM",
                MajorName = "Digital Marketing"
            });
        }
    }
}
