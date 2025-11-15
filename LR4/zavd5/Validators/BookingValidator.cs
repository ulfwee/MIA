using FluentValidation;
using MyWebApi.Models;

namespace MyWebApi.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Поле 'Name' є обов'язковим.")
                .MinimumLength(10).WithMessage("Назва має містити щонайменше 10 символів.")
                .Matches(@"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.\d{4}$");

            RuleFor(x => x.MasterId)
                .NotEmpty().WithMessage("Поле 'MasterId' є обов'язковим.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Поле 'Status' є обов'язковим.")
                .IsInEnum();

            RuleFor(x => x.ServiceDetails)
                .NotNull().WithMessage("Поле 'ServiceDetails' має бути вказано.");
        }
    }
}