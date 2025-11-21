using FluentValidation;
using MyWebApi.Models;

namespace MyWebApi.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле 'Name' є обов'язковим.")
                .MinimumLength(3).WithMessage("Назва має містити щонайменше 3 символи.")
                .MaximumLength(100).WithMessage("Назва не може перевищувати 100 символів.");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Поле 'Age' є обов'язковим.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Поле 'Email' є обов'язковим.")
                .MinimumLength(11).WithMessage("Ел. пошта має містити щонайменше 11 символів.")
                .EmailAddress().WithMessage("Ел. пошта не того формату");   
                }
    }
}