using System;
using FluentValidation;
using Lykke.Service.DashboardStatistics.Client.Models;

namespace Lykke.Service.DashboardStatistics.Validations
{
    public class BasePeriodRequestValidator<T> : AbstractValidator<T>
        where T : BasePeriodRequestModel
    {
        public BasePeriodRequestValidator()
        {
            RuleFor(o => o.FromDate.Date)
                .NotEmpty()
                .WithMessage("FromDate is required")
                .LessThanOrEqualTo(x => DateTime.UtcNow.Date)
                .WithMessage("FromDate must be equal or earlier than today.");

            RuleFor(o => o.ToDate.Date)
                .NotEmpty()
                .WithMessage("ToDate is required")
                .GreaterThanOrEqualTo(x => x.FromDate.Date)
                .WithMessage("ToDate must be equal or later than FromDate.")
                .LessThanOrEqualTo(x => DateTime.UtcNow.Date)
                .WithMessage("ToDate must be equal or earlier than today.");
        }
    }
}
