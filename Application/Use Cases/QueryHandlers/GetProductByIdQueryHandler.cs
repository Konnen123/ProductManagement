using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetAsync(request.Id);
            return product is null 
                ? Result<ProductDto>.Failure(ProductErrors.NotFound(request.Id)) 
                : Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }
        catch (Exception e)
        {
            return Result<ProductDto>.Failure(ProductErrors.GetProductFailed(e.Message));
        }
    }
}