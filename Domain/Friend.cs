namespace Domain;

public class Friend
{
    public int Id { get; set; }
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Идентификатор пользователя, находящийся в списке друзей
    /// </summary>
    public int FriendId { get; set; }
}