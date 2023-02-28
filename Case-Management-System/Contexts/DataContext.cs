using Case_Management_System.MVVM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Case_Management_System.Contexts;

internal class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Elin\Desktop\ECutbildning\5_Datalagring\Project\Case-Management-System\Case-Management-System\Contexts\local_CMS.mdf;Integrated Security=True;Connect Timeout=30";

    #region constructors
    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    #endregion

    #region overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }
    #endregion

    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<CaseEntity> Cases { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
}
