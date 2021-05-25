using InfoTrackWebSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrackWebSearch.SearchEngine
{
    public interface ISearchHistory
    {
        IEnumerable<WebSearch> GetHistory();

        void AddSearch(WebSearch webSearch);

        void ClearSearch();
    }
}
