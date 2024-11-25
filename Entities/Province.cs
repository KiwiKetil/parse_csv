namespace ParseCsv.Entities;
public class Province
{
    public required string ProvinceName { get; set; }
    public required string Abbreviation { get; set; }

    public override string ToString()
    {
        return $"{ProvinceName}, {Abbreviation}";
    }
}
