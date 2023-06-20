
using WarehouseWebApi.Interfaces;

namespace WarehouseWebApi.Models
{
    public static class DefaultMethods
    {
        public static List<PalletModel> Pallets(int count, List<PalletModel> palletsFromDb)
        {
            var totalCount = 0;
            var newPallets = new List<PalletModel>();

            foreach(var p in palletsFromDb)
            {
                if(totalCount > count)
                {
                    break;
                }
                totalCount += p.Ilość;
                newPallets.Add(p);
            }

            return new List<PalletModel>(newPallets);
        }
        public static bool CheckPalletNumber(long palletNumber, ISqlInterface repo)
        {
            var result = false;
            try
            {
                var pallet = repo.GetPalletByNumAsync(palletNumber).Result;
                if (pallet is PalletModel)
                {
                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
    }
}
