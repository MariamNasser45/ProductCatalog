using ProductCatalog.Models;

namespace ProductCatalog.Repo
{
    public interface ICategoryRepo
    {
        public bool CheckCategoryExistance(int CategoryId);

        public List<Category> GetAll();

    }
}
