namespace Domain;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public int Likes { get; set; }
    public DateTime CommentDate { get; set; }

    /// <summary>
    /// Пост, к которому написан комментарий
    /// </summary>
    public Post Post { get; set; } = new();

    /// <summary>
    /// Пользователь, который написал комментарий к посту
    /// </summary>
    public User User { get; set; } = new();
}