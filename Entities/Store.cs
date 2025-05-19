namespace dovandung0300467.Entities;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public TimeSpan OpeningHour { get; set; }
    public TimeSpan ClosingHour { get; set; }

    public virtual ICollection<StoreSupplier> StoreSuppliers { get; set; } = new List<StoreSupplier>();
}