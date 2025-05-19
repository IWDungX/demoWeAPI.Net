namespace dovandung0300467.Entities;

public class Supplier
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public virtual ICollection<StoreSupplier> StoreSuppliers { get; set; } = new List<StoreSupplier>();
}