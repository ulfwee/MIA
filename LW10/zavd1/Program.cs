using Microsoft.Win32;

public interface IPrototype
{
    IPrototype Clone();
}

public class SmartParking : IPrototype
{
    public string Name { get; set; }      
    public int TotalSpaces { get; set; }
    public int FreeSpaces { get; set; }  
    public List<string> Zones { get; set; }


   public SmartParking(string name, int totspaces, int freespaces)
    {
            Name = name;
        TotalSpaces = totspaces;    
        FreeSpaces = freespaces;
    }

    public IPrototype Clone()
    {
        var copy = (SmartParking)this.MemberwiseClone();
        copy.Zones = new List<string>(Zones);
        return copy;
    }

    public override string ToString()
    {
        return $"{Name} | Total spaces: {TotalSpaces} | Free spaces: {FreeSpaces} | Zones: [{string.Join(", ", Zones)}]";
    }
}

public class SmartParkingRegister
{
    private readonly Dictionary<string, SmartParking> _prototypes = new();

    public void Register(string key, SmartParking smartParking)
    {
        _prototypes[key] = smartParking;
    }

    public SmartParking Create(string key)
    {
        if (!_prototypes.ContainsKey(key))
            throw new ArgumentException($"Prototype '{key}' not found.");

        return (SmartParking)_prototypes[key].Clone();
    }
}

class Program
{
    static void Main()
    {
        var registry = new SmartParkingRegister();


        registry.Register("underground", new SmartParking("Underground Parking", 200, 200)
        {
            Zones = new List<string> { "U1", "U2", "U3" }
        });

        registry.Register("mall", new SmartParking("Mall Parking", 350, 350)
        {
            Zones = new List<string> { "A", "B", "C", "VIP" }
        });

        var under1 = registry.Create("underground");
        under1.Name = "Underground Parking 2";
        under1.Zones.Add("U4");

        var mall1 = registry.Create("mall");
        mall1.Name = "Mall Parking 2";
        mall1.Zones.Add("D");

        Console.WriteLine(under1);
        Console.WriteLine(mall1);
    }
}