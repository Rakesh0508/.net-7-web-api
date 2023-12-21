using System.ComponentModel.DataAnnotations;

namespace WorldAPI.DTO.Country
{
    public class UpdateCountryDto
    {

        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; }
    }
}
