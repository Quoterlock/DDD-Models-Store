using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DddModelsStore.DataAccess.Entities;

[Table("product")]
public class ProductEntity
{
    [Key]
    [Column("id")]
    public string Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("model_id")]
    public string ModelId { get; set; }
    [Column("owner_id")]
    public string OwnerId { get; set; }
    public CategoryEntity Category { get; set; }
    [Column("category_id")] 
    public string CategoryId { get; set; }
    [Column("creation_date")]
    public DateTime Created { get; set; }
    [Column("last_modification_date")]
    public DateTime Modified { get; set; }
}