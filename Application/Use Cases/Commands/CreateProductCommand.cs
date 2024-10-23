using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : BaseProductCommand, IRequest<Result<Guid>>
    {
    }
}
