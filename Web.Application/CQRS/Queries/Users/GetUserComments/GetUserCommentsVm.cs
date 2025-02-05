namespace Web.Application.CQRS.Queries.Users.GetUserComments;

public class GetUserCommentsVm
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }
    public string PostText { get; set; }
    public int LikesCount { get; set; }
    public bool IsLiked { get; set; }
    public string CommentDateHumanized { get; set; }
    public DateTime CommentDate { get; set; }
}