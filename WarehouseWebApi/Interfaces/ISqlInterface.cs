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
        Task<NewOrder>CreateOrderAsync(string localizationName, int count, int igredientNumber);
        Task<IEnumerable<PalletModel>> LocalizationCheckAsync(string machineName);
        Task SaveChangesAsync();
    }
}
