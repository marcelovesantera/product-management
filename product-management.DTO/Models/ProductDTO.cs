namespace product_management.DTO.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }

    public class ProductInsertDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }

    public class ProductUpdateDTO
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
