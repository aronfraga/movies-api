using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Controllers {
    // Limpiar controladores!!
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {

        private readonly ICategoryRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository ctRepo, IMapper mapper) {
            _ctRepo = ctRepo;
            _mapper = mapper;   
        }

        [HttpGet]
        public IActionResult GetCategories() {
            try {
                var response = _ctRepo.GetCategories();
                var responseDto = new List<CategoryDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<CategoryDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch(Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id) {
            try {
                var response = _ctRepo.GetCategory(id);
                if (response == null) throw new Exception("Category does not exists");
                var responseDto = _mapper.Map<CategoryDto>(response);

                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto ) {
            try {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (createCategoryDto== null) return BadRequest(ModelState);
                if (_ctRepo.ExistCategory(createCategoryDto.Name)) {
                    ModelState.AddModelError("", "The Category is already in the database");
                    return BadRequest(ModelState);
                }

                var response = _mapper.Map<Category>(createCategoryDto);
                if (!_ctRepo.CreateCategory(response)) { 
                    ModelState.AddModelError("", "Something went wrong");
                    return StatusCode(500, ModelState);
                }

                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        //prueba de ruteo diferente
        [HttpPatch("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(201, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto) {
                
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (categoryDto == null || id != categoryDto.Id) return BadRequest(ModelState);

            var response = _mapper.Map<Category>(categoryDto);

            if (!_ctRepo.UpdateCategory(response)) {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            if (!_ctRepo.ExistCategory(id)) return NotFound();

            var response = _ctRepo.GetCategory(id);

            if (!_ctRepo.DeleteCategory(response)) {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }





    }
}
