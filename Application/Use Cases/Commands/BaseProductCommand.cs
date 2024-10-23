namespace Application.Use_Cases.Commands;

public abstract class BaseProductCommand
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal TVA { get; set; }
}