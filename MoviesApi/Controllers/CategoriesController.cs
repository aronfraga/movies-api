using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Models.Dtos;
using MoviesApi.Repository.IRepository;

namespace MoviesApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;   
        }

        [HttpGet]
        public IActionResult GetCategories() {
            try {
                var response = _repository.GetCategories();
                var responseDto = new List<CategoryDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<CategoryDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch(Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpGet("{order:bool}")]
        public IActionResult GetCategories(bool order) {
            try {
                var response = _repository.GetCategories(order);
                var responseDto = new List<CategoryDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<CategoryDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id) {
            try {
                var response = _repository.GetCategory(id);
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
                if (!ModelState.IsValid) throw new Exception("The model is not correct");
                var responseDto = _mapper.Map<Category>(createCategoryDto);
                var response = _repository.CreateCategory(responseDto);
                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message, ModelEntry = ModelState });
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryDto) {
            try {
                if (!ModelState.IsValid) throw new Exception("The model is not correct");
                var responseDto = _mapper.Map<Category>(categoryDto);
                var response = _repository.UpdateCategory(responseDto);
                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message, ModelEntry = ModelState });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            try {
                var response = _repository.DeleteCategory(id);
                return StatusCode(200, new { request_status = "successful", response = response });
            } catch(Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }





    }
}
