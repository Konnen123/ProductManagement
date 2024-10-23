using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries;

public class GetProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>
{
}