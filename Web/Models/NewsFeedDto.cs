using Domain;

namespace Web.Models;

public class NewsFeedDto
{
    public string ImagePath { get; set; } = "";
    public List<Post> Posts { get; set; } = [];
}