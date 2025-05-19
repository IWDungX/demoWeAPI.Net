namespace dovandung0300467.Dtos;

public class PagedResultDto<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}