using AutoMapper;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;

namespace MoviesApi.MoviesMapper {
    public class MoviesMapper : Profile {

        public MoviesMapper() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
        }
    }
}
