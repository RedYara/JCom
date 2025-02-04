using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;
using Domain;

namespace Persistence;

public class DbContext(DbContextOptions<DbContext> options) : IdentityDbContext<User>(options), IDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
}