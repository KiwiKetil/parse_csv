namespace ParseCsv.ParseYield;
public class ProvinceInfo
{
    public required string Province { get; set; }
    public required string Abbreviation { get; set; }

    public override string ToString()
    {
        return $"{Province}, {Abbreviation}";
    }
}
