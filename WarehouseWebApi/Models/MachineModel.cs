using System.ComponentModel.DataAnnotations;
using WarehouseWebApi.Attributes;
using WarehouseWebApi.Interfaces;

namespace WarehouseWebApi.Models
{
    public class MachineModel
    {
        public int Id { get; set; }
        [Required]
        [ForbiddenValue("string")]
        public string Name { get; set; }
        public IEnumerable<PalletModel> Paletts { get; set; }

    }
}
