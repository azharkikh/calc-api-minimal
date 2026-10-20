using Microsoft.EntityFrameworkCore;

namespace CalcApiMinimal.History;

public class CalcDbContext : DbContext
{
    public CalcDbContext(DbContextOptions<CalcDbContext> options) : base(options)
    {
    }

    public DbSet<CalculationRecord> Calculations => Set<CalculationRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Имена таблицы и колонок заданы явно, чтобы в тестах было удобно писать SQL-запросы.
        modelBuilder.Entity<CalculationRecord>(entity =>
        {
            entity.ToTable("calculations");
            entity.Property(r => r.Id).HasColumnName("id");
            entity.Property(r => r.Operation).HasColumnName("operation").HasMaxLength(32);
            entity.Property(r => r.A).HasColumnName("a");
            entity.Property(r => r.B).HasColumnName("b");
            entity.Property(r => r.Result).HasColumnName("result");
            entity.Property(r => r.CreatedAt).HasColumnName("created_at");
        });
    }
}
