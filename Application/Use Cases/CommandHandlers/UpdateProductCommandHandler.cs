using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        try
        {
            if (product is null)
            {
                return Result.Failure(ProductErrors.ValidationFailed("Product is null"));
            }

            await _productRepository.UpdateAsync(product);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(ProductErrors.UpdateProductFailed(e.Message));
        }
    }
}