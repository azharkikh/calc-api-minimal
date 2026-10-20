namespace CalcApiMinimal.History;

public interface ICalculationHistory
{
    Task<CalculationRecord> AddAsync(string operation, double a, double b, double result);
    Task<CalculationRecord?> GetAsync(long id);
    Task<IReadOnlyList<CalculationRecord>> GetLatestAsync(int limit);

    /// <summary>
    /// Удаляет запись. Возвращает false, если записи с таким id нет.
    /// </summary>
    Task<bool> DeleteAsync(long id);
}
