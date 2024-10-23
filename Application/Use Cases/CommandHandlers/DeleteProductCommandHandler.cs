using Application.Errors;
using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetAsync(request.Id);
            if (product is null)
            {
                return Result.Failure(ProductErrors.NotFound(request.Id));
            }

            await _productRepository.DeleteAsync(product.Id);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(ProductErrors.DeleteProductFailed(e.Message));
        }
    }
}