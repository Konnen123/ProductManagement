﻿using FluentValidation;
using Infrastructure.Persistance;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommandValidator : BaseProductCommandValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(ApplicationDbContext context) : base(context)
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
