using Microsoft.AspNetCore.Routing.Constraints;
using MoviesApi.Models;

namespace MoviesApi.Repository.IRepository {
    public interface ICategoryRepository {
        
        ICollection<Category> GetCategories();

        ICollection<Category> GetCategories(bool order);

        Category GetCategory(int id);
        
        bool ExistCategory(string name);

        bool ExistCategory(int id);

        string CreateCategory(Category category);

        string UpdateCategory(Category category);

        string DeleteCategory(int id);

    }
}
