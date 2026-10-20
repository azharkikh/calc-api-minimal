using Microsoft.EntityFrameworkCore;

namespace CalcApiMinimal.History;

public class EfCalculationHistory : ICalculationHistory
{
    private readonly CalcDbContext _db;

    public EfCalculationHistory(CalcDbContext db)
    {
        _db = db;
    }

    public async Task<CalculationRecord> AddAsync(string operation, double a, double b, double result)
    {
        var record = new CalculationRecord
        {
            Operation = operation,
            A = a,
            B = b,
            Result = result,
            CreatedAt = DateTime.UtcNow
        };

        _db.Calculations.Add(record);
        await _db.SaveChangesAsync();
        return record;
    }

    public Task<CalculationRecord?> GetAsync(long id)
    {
        return _db.Calculations.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyList<CalculationRecord>> GetLatestAsync(int limit)
    {
        return await _db.Calculations.AsNoTracking()
            .OrderByDescending(r => r.Id)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var deleted = await _db.Calculations.Where(r => r.Id == id).ExecuteDeleteAsync();
        return deleted > 0;
    }
}
