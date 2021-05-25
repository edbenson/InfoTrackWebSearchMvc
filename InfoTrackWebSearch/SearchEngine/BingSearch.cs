using InfoTrackWebSearch.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoTrackWebSearch.SearchEngine
{
    public class BingSearch : SearchServiceBase, ISearchService
    {
        public BingSearch(ISearchHistory searchHistory) : base(searchHistory)
        {
        }

        public async Task<WebSearch> Search(WebSearch webSearch)
        {
            var uri = new Uri($"https://www.bing.co.uk/search?count={webSearch.SearchCount}&q={webSearch.SearchQuery}", UriKind.Absolute);

            webSearch.SearchTime = DateTime.Now;
            var client = new HttpClient();
            var httpResponse = await client.GetAsync(uri);
            var searchResult = await httpResponse.Content.ReadAsStringAsync();

            // Chop the result URLs 
            var matches = Regex.Matches(searchResult, "<a href=(.+?>)");

            // Look for the positions of the target URLs in the search results
            webSearch.FoundPositions = matches
                .Select((s, i) => (s.Value, i))
                .Where(m => m.Value.Contains(webSearch.ScanForUrl, StringComparison.InvariantCultureIgnoreCase))
                .Select(m => m.i + 1)
                .ToArray();

            _searchHistory.AddSearch(webSearch);
            return webSearch;
        }
    }
}
