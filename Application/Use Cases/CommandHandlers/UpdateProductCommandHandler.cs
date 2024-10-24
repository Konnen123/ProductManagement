using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        try
        {
            if (product is null)
            {
                return Result<Unit>.Failure(ProductErrors.ValidationFailed("Product is null"));
            }

            await _productRepository.UpdateAsync(product);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Failure(ProductErrors.UpdateProductFailed(e.Message));
        }
    }
}