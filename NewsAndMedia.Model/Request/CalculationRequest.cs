using System.ComponentModel.DataAnnotations;

namespace NewsAndMedia.Model.Request
{
    public class CalculationRequest
    {
        [Required]
        public decimal? Input { get; set; }
    }
}
