using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;
using MoviesApi.Repository.IRepository;
using System.Data;

namespace MoviesApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase {
        
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetMovies() {
            try {
                var response = _repository.GetMovies();
                var responseDto = new List<MovieDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<MovieDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("{order:bool}")]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetMovies(bool order) {
            try {
                var response = _repository.GetMovies(order);
                var responseDto = new List<CategoryDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<CategoryDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetMovie(int id) {
            try {
                var response = _repository.GetMovie(id);
                if (response == null) throw new Exception("Category does not exists");
                var responseDto = _mapper.Map<MovieDto>(response);
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("GetMovieOnCategory/{categoryId:int}")]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetMovies(int categoryId) {
            try {
                var response = _repository.GetMoviesOnCategory(categoryId);
                var responseDto = new List<MovieDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<MovieDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("Search")]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetMovies(string name) {
            try {
                var response = _repository.SearchMovie(name.Trim());
                var responseDto = new List<MovieDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<MovieDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CrateMovie([FromBody] MovieDto movieDto) {
            try {
                if (!ModelState.IsValid) throw new Exception("The model is not correct");
                var responseDto = _mapper.Map<Movie>(movieDto);
                var response = _repository.CreateMovie(responseDto);
                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] MovieDto movieDto) {
            try {
                if (!ModelState.IsValid) throw new Exception("The model is not correct");
                var responseDto = _mapper.Map<Movie>(movieDto);
                var response = _repository.UpdateMovie(responseDto);
                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message, ModelEntry = ModelState });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            try {
                var response = _repository.DeleteMovie(id);
                return StatusCode(200, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

    }
}
