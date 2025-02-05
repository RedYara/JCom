namespace Web.Models.AccountDtoModels;

public class LoginModelDto
{

    public string Name { get; set; }
    public string Password { get; set; }
    public string ReturnUrl { get; set; } = "/";
}