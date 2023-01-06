using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Repository {
    public class CategoryRepository : ICategoryRepository {

        private readonly Context _dbcontext;

        public CategoryRepository(Context dbcontext) {
            _dbcontext = dbcontext;
        }

        public bool ExistCategory(string name) {
            bool value = _dbcontext.Categorys.Any(data => data.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistCategory(int id) {
            return _dbcontext.Categorys.Any(data => data.Id == id);
        }

        public string CreateCategory(Category category) {
            if (category == null) throw new Exception("The category can not be empty or null");
            if (ExistCategory(category.Name)) throw new Exception("The Category is already in the database");
            category.CreationDate = DateTime.Now;
            _dbcontext.Categorys.Add(category);
            _dbcontext.SaveChanges();
            return "The category was created";
        }
        
        public ICollection<Category> GetCategories() {
            return _dbcontext.Categorys.ToList();
        }

        public ICollection<Category> GetCategories(bool order) {
            if(order) return _dbcontext.Categorys.OrderBy(data => data.Name).ToList();
                return _dbcontext.Categorys.OrderByDescending(data => data.Name).ToList();
        }

        public Category GetCategory(int id) {
            return _dbcontext.Categorys.FirstOrDefault(data => data.Id == id);
        }

        public string UpdateCategory(Category category) {
            if (category.Id == null) throw new Exception("The id can not be empty or null");
            if (ExistCategory(category.Name)) throw new Exception("The Category is already in the database");
            category.CreationDate = DateTime.Now;
            _dbcontext.Categorys.Update(category);
            _dbcontext.SaveChanges();
            return "The category was updated";
        }

        public string DeleteCategory(int id) {
            var dbResponse = GetCategory(id);
            if (!ExistCategory(dbResponse.Name)) throw new Exception("The Category does not exist");
            _dbcontext.Categorys.Remove(dbResponse);
            _dbcontext.SaveChanges();
            return "The category was deleted";
        }

    }
}
