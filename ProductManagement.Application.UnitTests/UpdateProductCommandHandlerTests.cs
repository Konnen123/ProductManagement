using Application.DTOs;
using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace ProductManagement.Application.UnitTests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_UpdateProductCommandHandler_When_HandleIsCalled_Then_ProductShouldBeUpdatedAndReturnUnit()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                Price = 15,
                TVA = 8
            };

            var product = GenerateProduct(command);
            GenerateProductDto(product);

            repository.GetAsync(command.Id).Returns(product);
            var handler = new UpdateProductCommandHandler(repository, mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);
            await repository.Received(1).UpdateAsync(product);
        }

        private Product GenerateProduct(UpdateProductCommand command)
        {
            return new Product
            {
                Id = command.Id,
                Name = command.Name,
                Price = command.Price,
                TVA = command.TVA
            };
        }

        private void GenerateProductDto(Product product)
        {
            mapper.Map<Product>(Arg.Any<UpdateProductCommand>()).Returns(product);

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
