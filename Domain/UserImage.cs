namespace Domain;

/// <summary>
/// "Аватарка" пользователя
/// </summary>
public class UserImage
{
    public int Id { get; set; }
    /// <summary>
    /// Путь к изображению
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// Пользователь, которому принадлежит "аватарка"
    /// </summary>
    public User User { get; set; }
}