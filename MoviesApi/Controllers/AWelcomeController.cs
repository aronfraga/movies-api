using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models.Dtos;

namespace MoviesApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AWelcomeController : ControllerBase {

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default_20S")]
        public IActionResult Welcome() {
            try {
                string message = "Hello this is API About MOVIES, to see the documentation go to link -> https://github.com/aronfraga/movies-api/blob/main/README.md";
                return StatusCode(200, new { request_status = "successful", response = message });
            } catch (Exception ex) {
                return StatusCode(500, new { request_status = "unsuccessful", response = ex.Message });
            }
        }
    }
}
