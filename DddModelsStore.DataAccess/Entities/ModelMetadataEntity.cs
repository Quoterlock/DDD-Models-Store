using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DddModelsStore.DataAccess.Entities;

[Table("model_metadata")]
public class ModelMetadataEntity
{
    [Key]
    [Column("id")]
    public string Id { get; set; } = string.Empty;
    [Column("format")]
    public string Format { get; set; } = string.Empty;
    [Column("tris_count")]
    public int TrianglesCount { get; set; }
    [Column("texture_resolution")]
    public int TextureResolution { get; set; }
    [Column("size_bytes")]
    public long SizeBytes { get; set; }
}