namespace WarehouseWebApi.Models
{
    public class PalletModel
    {
        public int Id { get; set; }
        public long PalletNumber { get; set; }
        public string MaterialName { get; set; }
        public int IngredienNumber { get; set; }
        public int Ilość { get; set; }
        public string Localization { get; set; }
        public string Status { get; set; }
        public int? NewOrderId { get; set; }
    }
}
