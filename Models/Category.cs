using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Category
    {
        public int CategoryID { get; set;}

        [Display(Name = "Category Name")]
        public string categoryName { get; set;}
        public ICollection<Product> products { get; set;}
    }
}
