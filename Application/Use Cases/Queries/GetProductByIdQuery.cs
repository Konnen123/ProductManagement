using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries;

public class GetProductByIdQuery : IRequest<Result<ProductDto>>
{
    public Guid Id { get; set; }
}