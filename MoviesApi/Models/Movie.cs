using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models {
    public class Movie {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public enum EClassification { Seven, Thirteen, Seventeen, Eighteen }
        public EClassification Classification { get; set; }
        public DateTime CreationDate { get; set; }
        
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
