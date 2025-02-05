namespace Web.Models.CommentDtoModels;

public class AddCommentDto
{
    public int PostId { get; set; }
    public string Text { get; set; }
    public string UserId { get; set; }
}