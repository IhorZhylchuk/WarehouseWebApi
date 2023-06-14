using System.ComponentModel.DataAnnotations;

namespace WarehouseWebApi.Models
{
    public class PalletModel : DefaultModelForPallets
    {
        [Key]
        public int Id { get; set; }

    }
}
