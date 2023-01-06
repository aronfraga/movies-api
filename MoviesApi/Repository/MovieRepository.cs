using Azure;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Repository {
    public class MovieRepository : IMovieRepository {

        private readonly Context _dbcontext;

        public MovieRepository(Context dbcontext) {
            _dbcontext = dbcontext;
        }

        public bool ExistMovie(string name) {
            bool value = _dbcontext.Movies.Any(data => data.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistMovie(int id) {
            return _dbcontext.Movies.Any(data => data.Id == id);
        }

        public string CreateMovie(Movie movie) {
            if (movie == null) throw new Exception("The movie can not be empty or null");
            if (ExistMovie(movie.Name)) throw new Exception("The movie is already in the database");
            movie.CreationDate = DateTime.Now;
            _dbcontext.Movies.Add(movie);
            _dbcontext.SaveChanges();
            return "The movie was created";
        }

        public Movie GetMovie(int id) {
            return _dbcontext.Movies.FirstOrDefault(data => data.Id == id);
        }

        public ICollection<Movie> GetMovies() {
            return _dbcontext.Movies.ToList();
        }

        public ICollection<Movie> GetMovies(bool order) {
            if (order) return _dbcontext.Movies.OrderBy(data => data.Name).ToList();
            return _dbcontext.Movies.OrderByDescending(data => data.Name).ToList();
        }

        public ICollection<Movie> GetMoviesOnCategory(int Id) {
            var dbResponse = _dbcontext.Movies.Include(data => data.Category).Where(data => data.Id == Id).ToList();
            if (dbResponse == null) throw new Exception("No movies inside");
            return dbResponse;
        }

        public ICollection<Movie> SearchMovie(string name) {
            IQueryable<Movie> query = _dbcontext.Movies;
            if (!string.IsNullOrEmpty(name)) query = query.Where(data => data.Name.Contains(name) ||
                                                                        data.Description.Contains(name));

            return query.ToList();
        }

        public string UpdateMovie(Movie movie) {
            if (movie.Id == null) throw new Exception("The id of the movie can not be empty or null");
            if (ExistMovie(movie.Name)) throw new Exception("The movie is already in the database");
            movie.CreationDate = DateTime.Now;
            _dbcontext.Movies.Update(movie);
            _dbcontext.SaveChanges();
            return "The movie was updated";
        }


        public string DeleteMovie(int id) {
            var dbResponse = GetMovie(id);
            if (!ExistMovie(dbResponse.Name)) throw new Exception("The Movie does not exist");
            _dbcontext.Movies.Remove(dbResponse);
            _dbcontext.SaveChanges();
            return "The movie was deleted";
        }
    }
}
