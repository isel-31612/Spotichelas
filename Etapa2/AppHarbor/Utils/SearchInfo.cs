
namespace Utils
{
    public class SearchInfo
    {
        public int Count {get;set;}
        public int Max { get; set; }
        public int Offset { get; set; }

        public string Query { get; set; }
        public string Type { get; set; }
        public int Page { get; set; }

        public SearchInfo(int count, int max, int offset, string query, string type, int page)
        {
            Count = count;
            Max = max;
            Offset = offset;
            Query = query;
            Type = type;
            Page = page;
        }
    }
}