using System;
using System.Linq;

namespace InfoTrackWebSearch.Models
{
    public class WebSearch
    {
        public string SearchQuery { get; set; }
        public int SearchCount { get; set; }
        public string ScanForUrl { get; set; }
        public DateTime SearchTime { get; set; }
        public int[] FoundPositions { get; set; }

        public string Positions => FoundPositions?.Length == 0
            ? "None found" 
            : string.Join(", ", FoundPositions.Select(p => p.ToString()));

    }
}
