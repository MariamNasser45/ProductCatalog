using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Repo.RepoServices
{
    public class CategoryServices : ICategoryRepo
    {
        public CategoryServices(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public bool CheckCategoryExistance(int CategoryId)
        {
            return Context.Categories.Any(c=>c.CategoryID == CategoryId);
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }
    }
}
