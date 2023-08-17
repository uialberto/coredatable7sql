namespace AppWeb.Helpers.Config
{
    public class PageList<TElement> : List<TElement>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalFiltered { get; set; }
        public int TotalElements { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public int? NextPageIndex => HasNextPage ? PageIndex + 1 : (int?)null;
        public int? PreviousPageIndex => HasPreviousPage ? PageIndex - 1 : (int?)null;
        public PageList(List<TElement> items, int count, int pageIndex, int pageSize)
        {
            TotalFiltered = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PageList<TElement> Create(IEnumerable<TElement> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<TElement>(items, count, pageIndex, pageSize);
        }
    }
}
