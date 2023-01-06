using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Repository {
    public class CategoryRepository : ICategoryRepository {

        private readonly Context _db;

        public CategoryRepository(Context db) {
            _db = db;
        }
        
        public bool CreateCategory(Category category) {
            category.CreationDate = DateTime.Now;
            _db.Categorys.Add(category);
            _db.SaveChanges();
        }

        public bool DeleteCategory(Category category) {
            _db.Categorys.Remove(category);
            return Save();
        }

        public bool ExistCategory(string name) {
            bool value = _db.Categorys.Any(data => data.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistCategory(int id) {
            return _db.Categorys.Any(data => data.Id == id);
        }

        public ICollection<Category> GetCategories() {
            return _db.Categorys.OrderBy(data => data.Name).ToList();
        }

        public Category GetCategory(int id) {
            return _db.Categorys.FirstOrDefault(data => data.Id == id);
        }

        public bool UpdateCategory(Category category) {
            category.CreationDate = DateTime.Now;
            _db.Categorys.Update(category);
            return Save();
        }

    }
}
