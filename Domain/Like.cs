using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Like
{
    public int Id { get; set; }
    /// <summary>
    /// Пост, который был лайкнут
    /// </summary>
    public Post? Post { get; set; }
    /// <summary>
    /// Комментарий, который был лайкнут
    /// </summary>
    public Comment? Comment { get; set; }
    /// <summary>
    /// Пользователь, который лайкнул
    /// </summary>
    public User User { get; set; }
}