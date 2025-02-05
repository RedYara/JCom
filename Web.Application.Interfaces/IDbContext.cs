using Domain;
using Microsoft.EntityFrameworkCore;

namespace Web.Application.Interfaces;

public interface IDbContext
{
    DbSet<Comment> Comments { get; set; }
    DbSet<Like> Likes { get; set; }
    DbSet<Post> Posts { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserImage> UserImages { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
