using Case_Management_System.MVVM.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Case_Management_System.Contexts;

internal class DatabaseContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Elin\Desktop\ECutbildning\5_Datalagring\Project\Case-Management-System\Case-Management-System\Contexts\local_CMS.mdf;Integrated Security=True;Connect Timeout=30";

    #region constructors
    public DatabaseContext()
    {
        
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    #endregion

    #region start employees

    EmployeeEntity employeeFirst = new EmployeeEntity
    {
        Id = 1000,
        FirstName = "Ingen är vald.",
        LastName = "",
        NameInitials = "Ingen signatur vald."
    };

    EmployeeEntity employeeSecond = new EmployeeEntity
    {
        Id = 1001,
        FirstName = "Göran",
        LastName = "Persson",
        NameInitials = "GP"
    };    

    EmployeeEntity employeeThird = new EmployeeEntity
    {
        Id = 1002,
        FirstName = "Fredrik",
        LastName = "Reinfeldt",
        NameInitials = "FR"
    };  
    
    EmployeeEntity employeeFourth = new EmployeeEntity
    {
        Id = 1003,
        FirstName = "Stefan",
        LastName = "Löfven",
        NameInitials = "SL"
    };
    
    EmployeeEntity employeeFifth = new EmployeeEntity
    {
        Id = 1004,
        FirstName = "Magdalena",
        LastName = "Andersson",
        NameInitials = "MA"
    };

    EmployeeEntity employeeSixth = new EmployeeEntity
    {
        Id = 1005,
        FirstName = "Ulf",
        LastName = "Kristersson",
        NameInitials = "UK"
    };

    #endregion

    #region overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeEntity>()
            .Property(x => x.Id)
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
            .HasAnnotation("SqlServer:IdentitySeed", 1000);

        modelBuilder.Entity<EmployeeEntity>().HasData(employeeFirst);
        modelBuilder.Entity<EmployeeEntity>().HasData(employeeSecond);
        modelBuilder.Entity<EmployeeEntity>().HasData(employeeThird);
        modelBuilder.Entity<EmployeeEntity>().HasData(employeeFourth);
        modelBuilder.Entity<EmployeeEntity>().HasData(employeeFifth);
        modelBuilder.Entity<EmployeeEntity>().HasData(employeeSixth);
    }
    #endregion

    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<CaseEntity> Cases { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
}
