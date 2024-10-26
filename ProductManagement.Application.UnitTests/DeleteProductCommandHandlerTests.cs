using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace ProductManagement.Application.UnitTests
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public DeleteProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_DeleteProductCommandHandler_When_ProductExists_Then_ShouldReturnSuccessResult()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = Guid.NewGuid() };
            var product = GenerateProduct(command.Id);
            GenerateProductDto(product);

            repository.GetAsync(command.Id).Returns(product);

            var handler = new DeleteProductCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            await repository.Received(1).DeleteAsync(command.Id);
        }

        private Product GenerateProduct(Guid productId)
        {
            return new Product
            {
                Id = productId,
                Name = "Product to Delete",
                Price = 15,
                TVA = 7
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
