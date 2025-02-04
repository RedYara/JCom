using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public UserImage UserImage { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Friend> Friends { get; set; }
}