namespace Web.Models.AccountDtoModels;

public class RegisterModelDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public IFormFile Logo { get; set; }
}