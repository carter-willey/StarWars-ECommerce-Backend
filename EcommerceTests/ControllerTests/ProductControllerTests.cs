using eCommerceStarterCode.Interfaces;
using eCommerceStarterCode.Models;
using Moq;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using eCommerceStarterCode.Controllers;
using System.Collections.Generic;

namespace EcommerceTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> repositoryStub = new Mock<IProductRepository>();

        private readonly Random rand = new();

        [Fact]
        public async Task GetAllProducts_WithExistingProducts_ShouldReturnAListOfProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>() { CreateRandomProduct(), CreateRandomProduct(), CreateRandomProduct() };
            repositoryStub.Setup(repo => repo.GetAllProducts()).ReturnsAsync(expectedProducts);

            var controller = new ProductController(repositoryStub.Object);

            // Act
            var actualProductsResult = await controller.GetAllProducts();

            // Assert
            actualProductsResult.Should().BeEquivalentTo(
                expectedProducts,
                options => options.ComparingByMembers<Product>()
                );

        }
        [Fact]
        public async Task GetAllProducts_WithNoExistingProducts_ShouldReturnAnEmptyList()
        {
            // Arrange
            var emptyProductsList = new List<Product>();
            repositoryStub.Setup(repo => repo.GetAllProducts()).ReturnsAsync(emptyProductsList);

            var controller = new ProductController(repositoryStub.Object);

            // Act
            var actualProductsResult = await controller.GetAllProducts();

            // Assert
            actualProductsResult.Should().BeEquivalentTo(
                emptyProductsList
                );
        }

        [Fact]
        public async Task GetProduct_WithExistingProduct_ShouldReturnTheExistingProduct()
        {
            // Arrange
            var expectedProduct = new List<Product>() { CreateRandomProduct() };
            repositoryStub.Setup(repo => repo.GetProduct(It.IsAny<int>())).ReturnsAsync(expectedProduct);

            var controller = new ProductController(repositoryStub.Object);

            // Act
            var actualProductResult = await controller.GetProduct(It.IsAny<int>());

            // Assert
            actualProductResult.Should().BeEquivalentTo(
            expectedProduct,
            options => options.ComparingByMembers<Product>()
            );

        }

        [Fact]
        public async Task GetProduct_WithNonExistingProduct_ShouldReturnAnEmptyList()
        {
            // Arrange
            var emptyProductList = new List<Product>();
            repositoryStub.Setup(repo => repo.GetProduct(It.IsAny<int>())).ReturnsAsync(emptyProductList);

            var controller = new ProductController(repositoryStub.Object);

            // Act
            var actualProductResult = await controller.GetProduct(It.IsAny<int>());

            // Assert
            actualProductResult.Should().BeEquivalentTo(
            emptyProductList
            );

        }

        [Fact]
        public async Task GetUsersProducts_WithExistingProducts_ShouldReturnAListOfUsersProducts()
        {
            // Arrange
            var expectedUserProducts = new List<Product>() { CreateRandomProduct(), CreateRandomProduct(), CreateRandomProduct() };
            repositoryStub.Setup(repo => repo.GetUsersProducts(It.IsAny<string>())).ReturnsAsync(expectedUserProducts);
            var controller = new ProductController(repositoryStub.Object);

            // Act
            var actualUserProductsResult = await controller.GetUsersProducts(It.IsAny<string>());

            // Assert 
            actualUserProductsResult.Should().BeEquivalentTo(
                expectedUserProducts,
            options => options.ComparingByMembers<Product>()
                );

        }

        [Fact]
        public async Task GetUsersProducts_WithNonExistingProducts_ShouldReturnAnEmptyListOfUsersProducts()
        {
            // Arrange
            var expectedUserProducts = new List<Product>();
            repositoryStub.Setup(repo => repo.GetUsersProducts(It.IsAny<string>())).ReturnsAsync(expectedUserProducts);
            var controller = new ProductController(repositoryStub.Object);

            // Act
            var actualUserProductsResult = await controller.GetUsersProducts(It.IsAny<string>());

            // Assert 
            actualUserProductsResult.Should().BeEquivalentTo(
                expectedUserProducts);
        }

        [Fact]
        public async Task AddNewProduct_WithANonExistingProduct_ShouldReturnOkResult()
        {
            // Arrange
            var newProduct = CreateRandomProduct();
            repositoryStub.Setup(repo => repo.AddNewProduct(newProduct)).ReturnsAsync(true);

            // Act
            var controller = new ProductController(repositoryStub.Object);
            var result = await controller.AddNewProduct(newProduct);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task AddNewProduct_WithAnExistingProduct_ShouldReturnBadRequest()
        {
            // Arrange
            var newProduct = CreateRandomProduct();
            repositoryStub.Setup(repo => repo.AddNewProduct(newProduct)).ReturnsAsync(false);

            // Act
            var controller = new ProductController(repositoryStub.Object);
            var result = await controller.AddNewProduct(newProduct);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateUserProduct_WithExistingProduct_ShouldReturnOkResult()
        {
            Product existingProduct = CreateRandomProduct();
            // Arrange
            repositoryStub.Setup(repo => repo.UpdateUserProduct(It.IsAny<int>(), existingProduct)).ReturnsAsync(true);

            // Act
            var controller = new ProductController(repositoryStub.Object);
            var result = await controller.UpdateUserProduct(It.IsAny<int>(), existingProduct);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task UpdateUserProduct_WithNonExistingProduct_ShouldReturnBadRequest()
        {
            // Arrange
            Product existingProduct = CreateRandomProduct();
            repositoryStub.Setup(repo => repo.UpdateUserProduct(It.IsAny<int>(), existingProduct)).ReturnsAsync(false);

            // Act
            var controller = new ProductController(repositoryStub.Object);
            var result = await controller.UpdateUserProduct(It.IsAny<int>(), existingProduct);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteUserProduct_WithExistingProduct_ShouldReturnOkResult()
        {
            // Arrange
            Product existingProduct = CreateRandomProduct();
            repositoryStub.Setup(repo => repo.DeleteUserProduct(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var controller = new ProductController(repositoryStub.Object);
            var result = await controller.DeleteUserProduct(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<OkResult>();

        }

        [Fact]
        public async Task DeleteUserProduct_WithNonExistingProduct_ShouldReturnBadResult()
        {
            // Arrange
            Product existingProduct = CreateRandomProduct();
            repositoryStub.Setup(repo => repo.DeleteUserProduct(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var controller = new ProductController(repositoryStub.Object);
            var result = await controller.DeleteUserProduct(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }




        private Product CreateRandomProduct()
        {
            return new()
            {
                Name = "Test Product",
                Price = rand.Next(1000),
                Description = "Test Product Description",
                AverageRating = It.IsAny<int>(),
                CategoryId = It.IsAny<int>(),
                UserId = It.IsAny<string>(),
                Image = It.IsAny<string>(),
            };
        }
    }
}
