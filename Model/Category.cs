using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RetailStore.Model
{
    public class Category
    {
        [Key]
        public int CID { get; set; }

        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }

    
        public List<Product> Products { get; set; }
    }
}
