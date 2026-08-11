namespace Licenses.Dto
{
    public
 class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } =  new HashSet<T>();
        public int ItemsCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public static PagedResult<T> PaginationData(IEnumerable<T> data, int itemsCount, int page, int pageSize=10)
        {
            return new PagedResult<T>
            {
                ItemsCount = itemsCount,
                Items = data,
                TotalPages = (int)Math.Ceiling((double) itemsCount / pageSize),
                CurrentPage = page,
            };
        }
    }
}
