using MediatR;

namespace Application.Use_Cases.Commands;

public class DeleteProductCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}