namespace Domain;

/// <summary>
/// Пост
/// </summary>
public class Post
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public int Likes { get; set; } = 0;
    public DateTime PostDate { get; set; }
    /// <summary>
    /// Изображения, прикреплённые к посту
    /// </summary>
    public ICollection<PostImage> Images { get; set; } = [];
    /// <summary>
    /// Юзер, которому принадлежит пост
    /// </summary>
    public User User { get; set; } = new();
}