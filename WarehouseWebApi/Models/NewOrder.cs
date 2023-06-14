using System.ComponentModel.DataAnnotations;

namespace WarehouseWebApi.Models
{
    public class NewOrder :DefaultModelForNewOrder
    {
        [Key]
        public int Id { get; set; }
    }
}
