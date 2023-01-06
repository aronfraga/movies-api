using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models.Dtos {
    public class MovieDto {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name must be obligatory")]
        public string Name { get; set; }
        public string Img { get; set; }

        [Required(ErrorMessage = "The description must be obligatory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The duration must be obligatory")]
        public int Length { get; set; }
        public enum EClassification { Seven, Thirteen, Seventeen, Eighteen }
        public EClassification Classification { get; set; }
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }

    }
}
