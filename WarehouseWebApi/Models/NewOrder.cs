namespace WarehouseWebApi.Models
{
    public class NewOrder
    {
        public int Id { get; set; }
        public string Localization { get; set; }
        public int IngredientNumber { get; set; }
        public int Count { get; set; }
        public IEnumerable<PalletModel> Paletts { get; set; }
    }
}
