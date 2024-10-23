using FluentValidation;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Application.Use_Cases.Commands
{
    public class BaseProductCommandValidator<T> : AbstractValidator<T> where T : BaseProductCommand
    {
        private readonly ApplicationDbContext applicationDbContext;
        public BaseProductCommandValidator(ApplicationDbContext context)
        {
            applicationDbContext = context;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").
                MaximumLength(200).WithMessage("Name must be of 200 characters maximum length.").
                MustAsync(BeUniqueName).WithMessage("The product must be unique.");
            
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.").
                GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.").
                Must(BeValidDecimal).WithMessage("Price must have a maximum of 10 digits with 2 decimal places.");

            RuleFor(x => x.TVA).NotEmpty().
                 WithMessage("TVA is required.").
                 GreaterThanOrEqualTo(0).WithMessage("TVA must be a non-negative value.").
                 Must(BeValidDecimal).WithMessage("TVA must have a maximum of 5 digits with 2 decimal places.").
                 Must((product, tva) => tva <= product.Price).WithMessage("TVA must not be greater than the price.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !await applicationDbContext.Products.AnyAsync(p => p.Name == name, cancellationToken);
        }

        private bool BeValidDecimal(decimal value)
        { 
            var valueString = value.ToString("F2");
            return valueString.Length <= 10 && valueString.IndexOf('.') <= 8;
        }
    }
}
