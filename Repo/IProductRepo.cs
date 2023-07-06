using ProductCatalog.Models;
using System.Numerics;

namespace ProductCatalog.Repo
{
    public interface IProductRepo
    {
        
        public List<Product> GetAll();
        public List<Product> GetAllAvail();
        //public List<Product> GetByCategory(int CategoryId);
        public bool CheckProductExistance(int CategoryId);
        public Product GetById(int id);
        public void Insert(Product product);
        public void Update(int id, Product product);
        public void Delete(int id);

    }
}
