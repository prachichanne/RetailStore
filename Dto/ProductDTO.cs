using RetailStore.Model;
using System.ComponentModel.DataAnnotations;

namespace RetailStore.Dto
{
    public class ProductDTO
    {

        [Key]
        public int PID { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Input should be positive value")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ProductName { get; set; }

        [Required]
        [Range(0.01, int.MaxValue, ErrorMessage = "Input should be positive value")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Foreign Key


    }
}
