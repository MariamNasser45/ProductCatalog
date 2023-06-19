using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }= DateTime.Now;

        [StringLength(450)]
        public string CreatedBy { get; set; }
      
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        public int Price { get; set; }

        // category ForeignKey
        [ForeignKey("Categories")]
        public int? CategoryID { get; set; }
    }
}
