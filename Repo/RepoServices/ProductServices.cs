using ProductCatalog.Data;
using ProductCatalog.Models;
using System.Security.Claims;

namespace ProductCatalog.Repo.RepoServices
{
    public class ProductServices : IProductRepo
    {
        public ApplicationDbContext Context { get; }

        public ProductServices(ApplicationDbContext context)
        {
            Context = context;
        }


        public void Delete(int id)
        {
            var deleteProduct = Context.Products.FirstOrDefault(p => p.ProductID == id);

            Context.Remove(deleteProduct);
            
            Context.SaveChanges();
        }

        public List<Product> GetAllAvail()
        {
            return Context.Products.Where(c =>(DateTime.Now.Day- c.StartDate.Day) < c.Duration).ToList();

        }

        public List<Product> GetAll()
        {

            return Context.Products.ToList();
        }

        public Product GetById(int id)
        {

            return Context.Products.FirstOrDefault(p => p.ProductID == id);

        }

        public void Insert(Product product)
        {
            
            Context.Add(product);
            Context.SaveChanges();
        }

        public void Update(int id, Product product)
        {
            var Editproduct = Context.Products.FirstOrDefault(p => p.ProductID == id);

            Editproduct.ProductName = product.ProductName;
            Editproduct.Price = product.Price;
            Editproduct.Duration = product.Duration;
            Editproduct.StartDate = product.StartDate;
            Editproduct.CategoryID = product.CategoryID;
            Editproduct.CreationDate = product.CreationDate;

            Context.Update(Editproduct);
            Context.SaveChanges();

        }
    }
}
