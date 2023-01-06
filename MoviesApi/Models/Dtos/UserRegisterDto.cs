using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.Dtos {
    public class UserRegisterDto {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
