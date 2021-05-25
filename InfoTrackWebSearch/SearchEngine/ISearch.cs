using InfoTrackWebSearch.Models;
using System.Threading.Tasks;

namespace InfoTrackWebSearch.SearchEngine
{
    public interface ISearchService
    {
        Task<WebSearch> Search(WebSearch webSearch);
    }
}
