using Microsoft.EntityFrameworkCore;
using WarehouseWebApi.Interfaces;

namespace WarehouseWebApi.Models
{
    public class SqlRepo : ISqlInterface
    {
        private readonly ApplicationDbContex _dbContext;
        public SqlRepo(ApplicationDbContex dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreatePalletAsync(PalletModel pallet)
        {
            await _dbContext.AddAsync(pallet);
        }

        public async Task DeletePalletAsync(PalletModel pallet)
        {
           _dbContext.Pallets.Remove(pallet);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PalletModel> GetPalletByNumAsync(long palletNumber)
        {
            var pallet = await _dbContext.Pallets.Where(n => n.PalletNumber == palletNumber).FirstAsync();
            return pallet;
        }

        public async Task<IEnumerable<PalletModel>> GetPalletsAsync()
        {
            var pallets = await _dbContext.Pallets.ToListAsync();
            return pallets;
        }

        public async Task<List<IEnumerable<PalletModel>>> LocalizationCheck(string machineName)
        {
            var pallets = await _dbContext.Machines.Where(n => n.Name == machineName).Select(p => p.Paletts).ToListAsync();
            return pallets;
        }

        public async Task<NewOrder> Order(string localizationName, int count, int igredientNumber)
        {
            var pallet = await _dbContext.Pallets.Where(n => n.IngredienNumber == igredientNumber).FirstAsync();

            var newOrder = new NewOrder()
            {
                Count = count,
                Localization = localizationName,
                IngredientNumber = igredientNumber,
                Paletts = DefaultMethods.Pallets(igredientNumber, count, _dbContext),
            };
            return newOrder;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePalletAsync(long palletNumber, PalletModel pallet)
        {
            var palletFromDb = await _dbContext.Pallets.Where(n => n.PalletNumber == palletNumber).FirstAsync();

            palletFromDb.PalletNumber = pallet.PalletNumber;
            palletFromDb.NewOrderId = pallet.NewOrderId;
            palletFromDb.Status = pallet.Status;
            palletFromDb.MaterialName = pallet.MaterialName;
            palletFromDb.Ilość = pallet.Ilość;
            palletFromDb.IngredienNumber = pallet.IngredienNumber;
            palletFromDb.Localization = pallet.Localization;

            _dbContext.Update(palletFromDb);
            await _dbContext.SaveChangesAsync();
        }
    }
}
