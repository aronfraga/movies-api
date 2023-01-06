using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.Dtos {
    public class CategoryDto {

        public int Id { get; set; }

        [Required(ErrorMessage = "The name must be obligatory")]
        [MaxLength(60, ErrorMessage = "The max character length is 100!")]
        public string Name { get; set; }

    }
}
