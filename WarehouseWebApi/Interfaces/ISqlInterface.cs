using WarehouseWebApi.Models;

namespace WarehouseWebApi.Interfaces
{
    public interface ISqlInterface
    {
        Task<IEnumerable<PalletModel>> GetPalletsAsync();
        Task CreatePalletAsync(PalletModel pallet);
        Task<PalletModel> GetPalletByNumAsync(long palletNumber);
        Task UpdatePalletAsync(long palletNumber, PalletModel pallet);
        Task DeletePalletAsync(PalletModel pallet);
        Task<NewOrder> Order(string localizationName, int count, int igredientNumber);
        Task<List<IEnumerable<PalletModel>>> LocalizationCheck(string machineName);
        Task SaveChangesAsync();
    }
}
