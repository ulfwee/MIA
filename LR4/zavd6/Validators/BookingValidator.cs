using FluentValidation;
using MyWebApi.Entities;

namespace MyWebApi.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Поле 'Date' є обов'язковим.");

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