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
    }
    #endregion

    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<CaseEntity> Cases { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
}
