using FluentValidation;
using Infrastructure.Persistance;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommandValidator : BaseProductCommandValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(ApplicationDbContext context) : base(context)
        {
            RuleFor(b => b.Id).NotEmpty().Must(IsValidGuid).WithMessage("Id must be provided.");
        }

        private bool IsValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }
    }
}
