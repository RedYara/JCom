namespace Domain;

/// <summary>
/// Изображение, прикреплённое к посту
/// </summary>
public class PostImage
{
    public int Id { get; set; }
    /// <summary>
    /// Путь к изображению
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// Пост, к которому прикреплено изображение
    /// </summary>
    public Post Post { get; set; }
}