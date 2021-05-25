using InfoTrackWebSearch.Controllers;
using InfoTrackWebSearch.Models;
using InfoTrackWebSearch.SearchEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace XUnitTestWebSearch
{
    public class UnitTestController : IDisposable
    {
        private Mock<ISearchService> _mockSearchService;
        private ISearchService _searchService;
        private ILogger<HomeController> _logger;
        private SearchHistoryInMemory _searchHistoryService;

        // This constructor gets called to do setup BEFORE every test 
        public UnitTestController()
        {
            var mockLoggerMock = new Mock<ILogger<HomeController>>();
            _logger = mockLoggerMock.Object;

            // Use the in-memory Search History service
            _searchHistoryService = new SearchHistoryInMemory();

            // Mock the Search service
            _mockSearchService = new Mock<ISearchService>();
            _mockSearchService.Setup(x => x.Search(It.IsAny<WebSearch>())).ReturnsAsync(new WebSearch());
            _searchService = _mockSearchService.Object;
        }

        // Dispose gets called to do teardown AFTER every test 
        public void Dispose()
        {
        }

        [Fact]
        public async void FirstSearchReturnsIsFirstTimeTrue()
        {
            // Arrange
            var homeController = new HomeController(_logger, _searchService, _searchHistoryService);

            // Act 
            var firstSearch = await homeController.Search(new WebSearch()) as ViewResult;

            // Assert
            Assert.True((bool)firstSearch.ViewData["IsFirstTime"]);
            Assert.IsType<WebSearch>(firstSearch.Model);
        }

        [Fact]
        public async void SubsequentSearchReturnsIsFirstTimeFalse()
        {
            // Arrange
            var homeController = new HomeController(_logger, _searchService, _searchHistoryService);

            // Act 
            var firstSearch = await homeController.Search(new WebSearch()) as ViewResult;
            var secondSearch = await homeController.Search((WebSearch)firstSearch.Model) as ViewResult;

            // Assert
            Assert.False((bool)secondSearch.ViewData["IsFirstTime"]);
            Assert.IsType<WebSearch>(secondSearch.Model);
        }
    }
}
