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
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public CreateProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_CreateProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeCreatedAndReturnGuid()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Product 1",
                Price = 10,
                TVA = 7
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.AddAsync(product).Returns(product.Id);
            var handler = new CreateProductCommandHandler(repository, mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(product.Id);
        }

        private Product GenerateProduct(CreateProductCommand command)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Price = command.Price,
                TVA = command.TVA
            };
        }

        private void GenerateProductDto(Product product)
        {
            mapper.Map<Product>(Arg.Any<CreateProductCommand>()).Returns(product);

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
