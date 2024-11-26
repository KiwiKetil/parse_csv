namespace ParseCsv.Entities;
public class RgbColor
{
    public required string Name { get; set; }
    public required string Hex { get; set; }
    public required string Rgb { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Hex}, {Rgb}";
    }
}
