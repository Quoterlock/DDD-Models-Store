namespace DddModelsStore.BusinessLogic.Models;

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; } = [];
}