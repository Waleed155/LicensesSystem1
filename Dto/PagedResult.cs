namespace Licenses.Dto
{
    public
 class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } =  new HashSet<T>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public static PagedResult<T> PaginationData(IEnumerable<T> data, int page, int pageSize, int totalItems)
        {
            return new PagedResult<T>
            {
                Items = data,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                CurrentPage = page,
            };
        }
    }
}
