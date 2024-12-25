namespace DddModelsStore.BusinessLogic.Models;

public class ModelDetails
{
    public string ModelId { get; set; } = string.Empty;
    public SupportedModelFormat Format { get; set; }
    public int TrianglesCount { get; set; }
    public int TextureResolution { get; set; } // NxN
    public long SizeBytes { get; set; }
}

public enum SupportedModelFormat
{
    Obj,
    Fbx
}