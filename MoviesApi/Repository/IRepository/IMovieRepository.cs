using MoviesApi.Models;
using MoviesApi.Models.Dtos;

namespace MoviesApi.Repository.IRepository {
    public interface IMovieRepository {

        ICollection<Movie> GetMovies();

        ICollection<Movie> GetMovies(bool order);

        Movie GetMovie(int id);

        bool ExistMovie(string name);

        bool ExistMovie(int id);

        string CreateMovie(Movie movie);

        string UpdateMovie(Movie movie);

        string DeleteMovie(int id);

        ICollection<Movie> GetMoviesOnCategory(int Id);

        ICollection<Movie> SearchMovie(string name);


    }
}
