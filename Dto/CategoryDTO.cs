using RetailStore.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RetailStore.Dto
{
    public class CategoryDTO
    {
        [Key]
        public int CID { get; set; }

        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }


        [JsonIgnore]
        public List<ProductDTO>? Products { get; set; }
    }
}
