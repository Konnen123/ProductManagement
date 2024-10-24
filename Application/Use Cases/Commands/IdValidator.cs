using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Commands
{
    public class IdValidator<T> : AbstractValidator<T> where T:IdCommand
    {
        public IdValidator()
        {
            RuleFor(b => b.Id).NotEmpty().WithMessage("Id must be provided.")
                     .Must(IsValidGuid).WithMessage("Id must be of valid format");
        }

        private bool IsValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }
    }
}
