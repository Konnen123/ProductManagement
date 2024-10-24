using MediatR;

namespace Application.Use_Cases.Commands;

public class DeleteProductCommand : IdCommand, IRequest<Result<Unit>>
{
}