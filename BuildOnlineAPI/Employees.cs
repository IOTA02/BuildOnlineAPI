using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BuildOnlineAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; } = null!;
        public DbSet<Positions> Position { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public static IConfigurationRoot Configuration { get; set; }

        public static string ConnectionString
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                Configuration = builder.Build();
                return Configuration.GetConnectionString("DefaultConnection");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }
    }
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public short PositionId { get; set; }
        public int Salary { get; set; }
        public bool IsActive { get; set; }
    }
    public class Positions
    {
        [Key]
        public int Id { get; set; }
        public string PositionName { get; set; }
    }
}
