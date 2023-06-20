using System.ComponentModel.DataAnnotations;
using WarehouseWebApi.Attributes;
using WarehouseWebApi.Interfaces;

namespace WarehouseWebApi.Models
{
    public class DefaultModelForPallets
    {
        [Required]
        [Range(1000000000,9999999999, ErrorMessage = "The length must be 10 digits, example - '3236713589'")]
        public long PalletNumber { get; set; }
        [Required]
        [MinLength(5), MaxLength(20)]
        [ForbiddenValue("string")]
        public string MaterialName { get; set; }
        [Required]
        [Range(4400000, 4499999, ErrorMessage = "The length must be 7 digits, example - '44022357'")]
        public int IngredienNumber { get; set; }
        [Required]
        [MinValue(0)]
        public int Ilość { get; set; }
        [Required]
        [MinLength(3), MaxLength(20)]
        [ForbiddenValue("string")]
        public string Localization { get; set; }
  
    }
}
