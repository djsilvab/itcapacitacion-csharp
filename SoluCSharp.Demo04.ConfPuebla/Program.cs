using static System.Console;

WriteLine("Demo de Net Conf 2020 - Puebla");

var productA = new Product
                {
                    Id = 1,
                    Name = "Azúcar",
                    UnitPrice = 25,
                    UnitsInStock = 100
                };

var productB = new Product
{
    Id = 1,
    Name = "Azúcar",
    UnitPrice = 25,
    UnitsInStock = 100
};

ProductDto productDto = new ProductDto(1, "Naranja");
ProductDto productDtoA = new ProductDto(1, "Naranja");

WriteLine($"Los records iguales? { productDto.Equals(productDtoA) } ");
WriteLine($"Las clases iguales? { productA.Equals(productB) } ");

WriteLine(productA);
WriteLine(productDtoA);

Vegetal03Dto v3 = new Vegetal03Dto(10, "lechuga", 15.55m);
WriteLine(v3);