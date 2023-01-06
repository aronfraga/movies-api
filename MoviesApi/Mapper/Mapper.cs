using AutoMapper;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;

namespace MoviesApi.Mapper {
    public class Mapper : Profile {

        public Mapper() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
        }
    }
}
