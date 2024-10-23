using Application.Errors;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            if(product is null)
            {
                return Result<Guid>.Failure(ProductErrors.ValidationFailed("Product is null"));
            }
            
            try
            {
                var returnedId = await repository.AddAsync(product);
                return Result<Guid>.Success(returnedId);
            }
            catch (Exception e)
            {
                return Result<Guid>.Failure(ProductErrors.CreateProductFailed(e.Message));
            }
        }
    }
}
    