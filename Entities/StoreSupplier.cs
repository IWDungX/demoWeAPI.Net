namespace dovandung0300467.Entities;

public class StoreSupplier
{
    public int StoreId { get; set; } // Khóa ngoại
    public int SupplierId { get; set; } // Khóa ngoại
    public float FriendshipLevel { get; set; } 

    public virtual Store Store { get; set; } = null!;
    public virtual Supplier Supplier { get; set; } = null!;
}