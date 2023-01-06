using MoviesApi.Models;

namespace MoviesApi.Repository.IRepository {
    public interface ICategoryRepository {
        
        ICollection<Category> GetCategories();
        
        Category GetCategory(int id);
        
        bool ExistCategory(string name);

        bool ExistCategory(int id);

        Category CreateCategory(Category category);

        Category UpdateCategory(Category category);

        Category DeleteCategory(Category category);

        bool Save();

    }
}
