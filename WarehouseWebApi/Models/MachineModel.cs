namespace WarehouseWebApi.Models
{
    public class MachineModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PalletModel> Paletts { get; set; }

    }
}
