using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.BusinessLogic.Models;
using DddModelsStore.BusinessLogic.Services;
using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;
using Moq;

namespace DddModelsStore.UnitTests.LogicTests;

public class CategoriesServiceTests
{
    [Fact]
    public async Task GetCategoryByIdTest()
    {
        // Arrange
        var repo = new Mock<IRepository<CategoryEntity>>();
        var mapper = new Mock<IMapper<Category, CategoryEntity>>();
        var sut = new CategoriesService(repo.Object, mapper.Object);

        var categoryId = "id";
        
        var expectedCategory = new Category{ Id = categoryId };
        var entity = new CategoryEntity { Id = categoryId }; 
        repo.Setup(m=>m.GetAsync(categoryId)).ReturnsAsync(entity);
        mapper.Setup(m=>m.MapAsync(entity)).ReturnsAsync(expectedCategory); 
        
        // Act
        var actual = await sut.GetCategoryByIdAsync(categoryId);
        
        // Assert
        Assert.Equal(actual, expectedCategory);
        mapper.Verify(m=>m.MapAsync(entity), Times.Once());
        repo.Verify(m=>m.GetAsync(categoryId), Times.Once());
    }
}