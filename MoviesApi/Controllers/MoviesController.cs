using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase {

        private readonly IMovieRepository _pelRepo;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository pelRepo, IMapper mapper) {
            _pelRepo = pelRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies() {
            try {
                var response = _pelRepo.GetMovies();
                var responseDto = new List<MovieDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<MovieDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id) {
            try {
                var response = _pelRepo.GetMovie(id);
                if (response == null) throw new Exception("Category does not exists");
                var responseDto = _mapper.Map<MovieDto>(response);

                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrateMovie([FromBody] MovieDto movieDto) {
            try {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (movieDto == null) return BadRequest(ModelState);
                if (_pelRepo.ExistMovie(movieDto.Name)) {
                    ModelState.AddModelError("", "The Category is already in the database");
                    return BadRequest(ModelState);
                }

                var response = _mapper.Map<Movie>(movieDto);
                if (!_pelRepo.CreateMovie(response)) {
                    ModelState.AddModelError("", "Something went wrong");
                    return StatusCode(500, ModelState);
                }

                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpPatch("{id:int}", Name = "UpdateMovie")]
        [ProducesResponseType(201, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] MovieDto movieDto) {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (movieDto == null || id != movieDto.Id) return BadRequest(ModelState);

            var response = _mapper.Map<Movie>(movieDto);

            if (!_pelRepo.UpdateMovie(response)) {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id) {
            if (!_pelRepo.ExistMovie(id)) return NotFound();

            var response = _pelRepo.GetMovie(id);

            if (!_pelRepo.DeleteMovie(response)) {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetMovieOnCategory/{categoryId:int}")]
        public IActionResult GetMovies(int categoryId) {
            try {
                var response = _pelRepo.GetMoviesOnCategory(categoryId);

                if(response == null) return NotFound();

                var responseDto = new List<MovieDto>();

                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<MovieDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpGet("Search")]
        public IActionResult GetMovies(string name) {
            try {
                var response = _pelRepo.SearchMovie(name.Trim());

                var responseDto = new List<MovieDto>();

                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<MovieDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

    }
}
