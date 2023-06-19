using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }= DateTime.Now;

        [StringLength(450)]
        public string CreatedBy { get; set; }
      
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]
        public int Duration { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Price { get; set; }

        // category ForeignKey
        [ForeignKey("Categories")]
        public int? CategoryID { get; set; }
        
    }
}
