﻿using FluentValidation;
using Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductCommandValidator : IdValidator<DeleteProductCommand>
    {
    }
}
