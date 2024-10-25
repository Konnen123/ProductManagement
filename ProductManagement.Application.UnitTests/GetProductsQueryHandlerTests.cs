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
    public class GetProductsQueryHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        public GetProductsQueryHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }
        [Fact]
        public void Given_GetProductsQueryHandler_When_HandleIsCalled_Then_AListOfProductsShouldBeReturned()
        {
            // Arrange
            List<Product> products = GenerateProducts();
            repository.GetAllAsync().Returns(products);

            var query = new GetProductsQuery();
            GenerateProductsDto(products);

            // Act
            var handler = new GetProductsQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        private void GenerateProductsDto(List<Product> products)
        {
            mapper.Map<List<ProductDto>>(products).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Id = products[0].Id,
                    Name = products[0].Name,
                    Price = products[0].Price,
                    TVA = products[0].TVA
                },
                new ProductDto
                {
                    Id = products[1].Id,
                    Name = products[1].Name,
                    Price = products[1].Price,
                    TVA = products[1].TVA
                }
            });
        }

        private List<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Price = 10.0m, 
                    TVA = 10
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Price = 20.0m,
                    TVA = 10
                }
            };
        }
    }
}