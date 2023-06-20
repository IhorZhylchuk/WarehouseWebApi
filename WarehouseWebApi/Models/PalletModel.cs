using System.ComponentModel.DataAnnotations;
using WarehouseWebApi.Attributes;
using WarehouseWebApi.Interfaces;

namespace WarehouseWebApi.Models
{
    public class PalletModel : DefaultModelForPallets
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
