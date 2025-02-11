using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web.Models.AccountDtoModels;

public class RegisterModelDto
{
    [Required(ErrorMessage = "Введите логин")]
    [Remote(action: "CheckUserName", controller: "Account", ErrorMessage = "Данный логин уже занят")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Введите тег пользователя")]
    [MinLength(4, ErrorMessage = "Тег пользователя должен быть длиной минимум 4 символа")]
    [RegularExpression("^(?=.*[a-zA-Z])[a-zA-Z0-9_.]*$", ErrorMessage = "Тег должен состоять из латиницы и может содержать в себе символы '-' '_' '.' ")]
    [Remote(action: "CheckTag", controller: "Account", ErrorMessage = "Данный тег уже занят")]
    public string UserTag { get; set; }

    [Required(ErrorMessage = "Введите имя")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Введите фамилию")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Укажите почтовый адрес")]
    public string Email { get; set; }

    public IFormFile Logo { get; set; }
}