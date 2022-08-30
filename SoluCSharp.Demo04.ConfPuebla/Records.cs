using System.Text;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }

    public static implicit operator ProductDto(Product p)
    {
        return new ProductDto(p.Id, p.Name);
    }
}

//Sintaxis Positional Records
public record ProductDto(int Id, string Name);
/*
{
    public ProductDto(int Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public int Id { get; }
    public string Name { get; }    
}
*/

public record VegetalDto : ProductDto
{
    public VegetalDto(int Id, string Name, decimal UnitPrice)
        : base(Id, Name) => this.UnitPrice = UnitPrice;    

    public decimal UnitPrice { get; }
}

public record Vegetal02Dto(int Id, string Name, decimal UnitPrice):ProductDto(Id,Name);

public record Vegetal03Dto(int Id, string Name, decimal UnitPrice) : ProductDto(Id, Name)
{
    public string GetMyName()
    {
        return $"I am {Name}";
    }

    public override string ToString()
    {
        StringBuilder sb = new ();
        base.PrintMembers(sb);
        return $"{sb.ToString()}, UnitPrice = {UnitPrice}";
    }
}
