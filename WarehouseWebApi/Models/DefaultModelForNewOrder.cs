namespace WarehouseWebApi.Models
{
    public class DefaultModelForNewOrder
    {
      
        public string Localization { get; set; }
        public int IngredientNumber { get; set; }
        public int Count { get; set; }
        public IEnumerable<PalletModel> Paletts { get; set; }
    }
}
