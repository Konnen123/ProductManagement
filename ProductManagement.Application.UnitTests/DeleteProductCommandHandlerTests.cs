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

        [Fact]
        public async Task Given_NonExistingProductId_When_HandleIsCalled_Then_ShouldReturnNotFoundResult()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = Guid.NewGuid() }; 

            repository.GetAsync(command.Id).Returns((Product)null); 
            var handler = new DeleteProductCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be($"The product with id: {command.Id} was not found.");
        }

        [Fact]
        public async Task Given_ExceptionThrownInDeleteAsync_When_HandleIsCalled_Then_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = Guid.NewGuid() };
            var product = GenerateProduct(command.Id);
            GenerateProductDto(product);

            repository.GetAsync(command.Id).Returns(product); 
            repository.DeleteAsync(product.Id).Returns(Task.FromException<Unit>(new Exception("Database error"))); 
            var handler = new DeleteProductCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Description.Should().Be("Database error"); 
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
