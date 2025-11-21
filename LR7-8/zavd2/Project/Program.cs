public enum Category
{
    Plumbing,
    Electrical,
    Flooring,
    Assembly
}
public class Master
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Category Category { get; set; }
    public double Ranking { get; set; }
}

