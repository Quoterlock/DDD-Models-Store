using DddModelsStore.DataAccess.Entities; 
namespace DddModelsStore.BusinessLogic.Models;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AppUser Owner { get; set; } 
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Category Category { get; set; }
    public ModelDetails Details { get; set; } = new ModelDetails();
}

public class NewProduct
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AppUser Owner { get; set; }
    public Stream ModelData { get; set; }
    public string Extension { get; set; } = string.Empty;
}