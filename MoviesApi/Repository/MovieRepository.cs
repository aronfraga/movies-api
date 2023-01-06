using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Repository {
    public class MovieRepository : IMovieRepository {

        private readonly Context _db;

        public MovieRepository(Context db) {
            _db = db;
        }

        public bool CreateMovie(Movie movie) {
            movie.CreationDate = DateTime.Now;
            _db.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie) {
            _db.Movies.Remove(movie);
            return Save();
        }

        public bool ExistMovie(string name) {
            bool value = _db.Movies.Any(data => data.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistMovie(int id) {
            return _db.Movies.Any(data => data.Id == id);
        }

        public ICollection<Movie> GetMovies() {
            return _db.Movies.OrderBy(data => data.Name).ToList();
        }

        public Movie GetMovie(int id) {
            return _db.Movies.FirstOrDefault(data => data.Id == id);
        }

        public bool Save() {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMovie(Movie movie) {
            movie.CreationDate = DateTime.Now;
            _db.Movies.Update(movie);
            return Save();
        }

        public ICollection<Movie> GetMoviesOnCategory(int Id) {
            return _db.Movies.Include(data => data.Category).Where(data => data.Id == Id).ToList();
        }

        public ICollection<Movie> SearchMovie(string name) {
            IQueryable<Movie> query = _db.Movies;

            if(!string.IsNullOrEmpty(name)) query = query.Where(data => data.Name.Contains(name) || 
                                                                        data.Description.Contains(name));

            return query.ToList();
        }
    }
}
