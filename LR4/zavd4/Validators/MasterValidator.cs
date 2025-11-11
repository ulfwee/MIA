using FluentValidation;
using MyWebApi.Models;

namespace MyWebApi.Validators
{
    public class MasterValidator : AbstractValidator<Master>
    {
        public MasterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле 'Name' є обов'язковим.")
                .MinimumLength(3).WithMessage("Назва має містити щонайменше 3 символи.")
                .MaximumLength(100).WithMessage("Назва не може перевищувати 100 символів.");

            RuleFor(x => x.Category)
                .NotNull().WithMessage("Поле 'Category' має бути вказано.")
                .IsInEnum();

            RuleFor(x => x.Ranking)
                .NotNull().WithMessage("Поле 'ranking' має бути вказано.");
        }
    }
}