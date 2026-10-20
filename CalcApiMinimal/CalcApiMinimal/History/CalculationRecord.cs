namespace CalcApiMinimal.History;

public class CalculationRecord
{
    public long Id { get; set; }
    public string Operation { get; set; } = "";
    public double A { get; set; }
    public double B { get; set; }
    public double Result { get; set; }
    public DateTime CreatedAt { get; set; }
}
