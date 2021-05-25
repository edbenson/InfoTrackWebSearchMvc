using InfoTrackWebSearch.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoTrackWebSearch.SearchEngine
{
    public class GoogleSearch : SearchServiceBase, ISearchService
    {
        public GoogleSearch(ISearchHistory searchHistory) : base(searchHistory)
        {
        }

        public async Task<WebSearch> Search(WebSearch webSearch)
        {
            var uri = new Uri($"https://www.google.co.uk/search?num={webSearch.SearchCount}&q={webSearch.SearchQuery}&ucbcb=1", UriKind.Absolute);

            webSearch.SearchTime = DateTime.Now;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            var httpResponse = await client.GetAsync(uri);
            var searchResult = await httpResponse.Content.ReadAsStringAsync();

            // Chop the result URLs 
            var matches = Regex.Matches(searchResult, "<a href=\"/url(.+?>)");

            // Look for the positions of the target URLs in the search results
            webSearch.FoundPositions = matches
                .Select((s, i) => (s.Value, i))
                .Where(m => m.Value.Contains(webSearch.ScanForUrl, StringComparison.InvariantCultureIgnoreCase))
                .Select(m => m.i+1)
                .ToArray();

            _searchHistory.AddSearch(webSearch);
            return webSearch;
        }
    }
}
