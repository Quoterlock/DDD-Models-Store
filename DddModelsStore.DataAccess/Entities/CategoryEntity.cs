using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DddModelsStore.DataAccess.Entities;

[Table("category")]
public class CategoryEntity
{
    [Key]
    [Column("id")]
    public string Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    public ICollection<ProductEntity> Products { get; set; } = [];
}