using System.ComponentModel.DataAnnotations;

namespace Bai2.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required, StringLength(100)]

        public string Name { get; set; }

        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Guests { get; set; }
    }
}
