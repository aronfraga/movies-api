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
    public class UsersController : ControllerBase {

        private readonly IUserRepository _repository;
        protected Response _response;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper) {
            _repository = repository;
            this._response = new();
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetUsers() {
            try {
                var response = _repository.GetUsers();
                var responseDto = new List<UserDto>();
                foreach (var item in response) {
                    responseDto.Add(_mapper.Map<UserDto>(item));
                }
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult GetUser(int id) {
            try {
                var response = _repository.GetUser(id);
                if (response == null) throw new Exception("User does not exists");
                var responseDto = _mapper.Map<UserDto>(response);
                return StatusCode(200, new { request_status = "successful", response = responseDto });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userRegisterDto) {
            try {
                var response = await _repository.RegisterAsync(userRegisterDto);
                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto) {
            try {
                var response = await _repository.LoginAsync(userLoginDto);
                return StatusCode(201, new { request_status = "successful", response = response });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }

    }
}
