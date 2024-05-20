namespace ArtGallery.Utils { 
    public class PaginatedResponse<T>(IEnumerable<T> items, int pageIndex, int totalPages) {
        public IEnumerable<T> Items { get; } = items;
        public int PageIndex { get; } = pageIndex;
        public int TotalPages { get; } = totalPages;
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}