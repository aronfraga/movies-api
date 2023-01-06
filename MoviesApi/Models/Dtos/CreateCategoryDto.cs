using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.Dtos {
    public class CreateCategoryDto {

        [Required(ErrorMessage = "The name must be obligatory")]
        [MaxLength(60, ErrorMessage = "The max character length is 100!")]
        public string Name { get; set; }

    }
}
