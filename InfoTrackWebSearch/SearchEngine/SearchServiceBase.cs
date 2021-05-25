namespace InfoTrackWebSearch.SearchEngine
{
    public abstract class SearchServiceBase
    {
        protected ISearchHistory _searchHistory;

        protected SearchServiceBase(ISearchHistory searchHistory)
        {
            _searchHistory = searchHistory;
        }

    }
}
