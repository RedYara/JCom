namespace Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;

public class GetPostsVm
{
    public int PostId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; }
    public string Text { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public bool IsLiked { get; set; }
    public string PostDateHumanized { get; set; }
    public DateTime PostDate { get; set; }
}