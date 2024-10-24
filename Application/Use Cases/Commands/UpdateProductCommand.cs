using MediatR;

namespace Application.Use_Cases.Commands;

public class UpdateProductCommand : BaseProductCommand, IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}