using InfoTrackWebSearch.Models;
using InfoTrackWebSearch.SearchEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackWebSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;
        private readonly ISearchHistory _searchHistory;

        public HomeController(
            ILogger<HomeController> logger,
            ISearchService searchService,
            ISearchHistory searchHistory)
        {
            _logger = logger;
            _searchService = searchService;
            _searchHistory = searchHistory;
        }

        public async Task<IActionResult> Index(WebSearch webSearch)
        {
            var searchResult = await _searchService.Search(webSearch);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Search(WebSearch webSearch)
        {
            if (webSearch.SearchQuery == null)
            {
                ViewData["IsFirstTime"] = true;
                return View(new WebSearch
                {
                    SearchQuery = "Land registry searches",
                    ScanForUrl = "www.infotrack.co.uk",
                    SearchCount = 100,
                    FoundPositions=new int[0]
                });
            }
            ViewData["IsFirstTime"] = false;
            var searchResult = await _searchService.Search(webSearch);
            return View(searchResult);
        }

        public IActionResult Display()
        {
            return View(new WebSearch
            {
                SearchQuery = "Land registry searches",
                SearchCount = 100,
                ScanForUrl = "www.infotrack.com"
            });
        }

        public IActionResult History()
        {
            var history = _searchHistory.GetHistory().ToArray();
            return View(history);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
