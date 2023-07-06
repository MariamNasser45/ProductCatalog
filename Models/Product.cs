using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; }

        [StringLength(450)]
        [Display(Name = "Created By")]
        public string createdBy { get; set; }
      
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }

        [Required]
        [Range(minimum:1,maximum:int.MaxValue)]
        [Display(Name ="Duration in Days")]
        public int duration { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int price { get; set; }

        // category ForeignKey
        //[ForeignKey("Categories")]
        public Category? category { get; set; } // By using Navigational property don't need to use DataAnnotation[ForeignKey("Categories")]
        public int CategoryID { get; set; }
        
    }
}
