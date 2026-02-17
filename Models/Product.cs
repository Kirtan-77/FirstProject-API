using System.ComponentModel.DataAnnotations;

namespace FirstProject.Api.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public DateTime MFG { get; set; }
        public DateTime Exp { get; set; }
        public decimal Mrp { get; set; }
    }

    public class CreateProductDto
    {
        // No Id - it's auto-generated
        public string Name { get; set; }
        public string Serial { get; set; }
        // ... etc
    }
}
