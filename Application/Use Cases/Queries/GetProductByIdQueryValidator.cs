using Application.Use_Cases.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Queries
{
    public class GetProductByIdQueryValidator : IdValidator<GetProductByIdQuery>
    {
    }
}
