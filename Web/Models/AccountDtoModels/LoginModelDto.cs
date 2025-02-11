using System.ComponentModel.DataAnnotations;

namespace Web.Models.AccountDtoModels;

public class LoginModelDto
{

    [Required(ErrorMessage = "Введите логин")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
    public string ReturnUrl { get; set; } = "/";
}