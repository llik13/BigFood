using System.ComponentModel.DataAnnotations;

namespace Registration.Models
{
    public class RegisterDeliverModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string VehicleType { get; set; }

        public string? LicensePlate { get; set; }

        [Required]
        public bool IsAvailable { get; set; } 
    }
}
