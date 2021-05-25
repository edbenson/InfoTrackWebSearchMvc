using InfoTrackWebSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoTrackWebSearch.SearchEngine
{
    public class SearchHistoryInMemory : ISearchHistory
    {
        private List<WebSearch> _searchHistory = new List<WebSearch>();

        public void AddSearch(WebSearch webSearch)
        {
            _searchHistory.Insert(0, webSearch);
        }

        public void ClearSearch()
        {
            _searchHistory.Clear();
        }

        public IEnumerable<WebSearch> GetHistory()
        {
            return _searchHistory;
        }
    }
}
