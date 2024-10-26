using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace ProductManagement.Application.UnitTests
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        public GetProductByIdQueryHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_GetProductByIdQueryHandler_When_HandleIsCalled_Then_ReturnedProductShouldMatchQueryProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = GenerateProduct(productId);
            repository.GetAsync(productId).Returns(product);
            var query = new GetProductByIdQuery { Id = productId };
            GenerateProductDto(product);

            // Act
            var handler = new GetProductByIdQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.Value.Id.ToString().Should().Be(productId.ToString());

        }


        private Product GenerateProduct(Guid guid)
        {
            return new Product
            {
                Id = guid,
                Name = "Product",
                Price = 10,
                TVA = 19
            };
        }

        private void GenerateProductDto(Product product)
        {
            mapper.Map<ProductDto>(product).Returns(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                TVA = product.TVA
            });

        }

    }
}
