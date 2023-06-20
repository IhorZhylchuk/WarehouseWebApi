using System.ComponentModel.DataAnnotations;
using WarehouseWebApi.Attributes;
using WarehouseWebApi.Interfaces;

namespace WarehouseWebApi.Models
{
    public class DefaultModelForNewOrder
    {
        [Required]
        [MinLength(5), MaxLength(20)]
        [ForbiddenValue("string")]
        public string Localization { get; set; }
        [Required]
        [Range(4400000, 4499999, ErrorMessage = "The length must be 7 digits, example - '44022357'")]
        public int IngredientNumber { get; set; }
        [Required]
        [MinValue(0)]   
        public int Count { get; set; }
        public IEnumerable<PalletModel> Paletts { get; set; }
    }
}
