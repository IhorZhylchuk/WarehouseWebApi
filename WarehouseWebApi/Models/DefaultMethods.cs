
namespace WarehouseWebApi.Models
{
    public static class DefaultMethods
    {
        public static List<PalletModel> Pallets(int ingredientNumber, int count, ApplicationDbContex dbContex)
        {
            var totalCount = 0;
            var palletsFromDb = dbContex.Pallets.Where(n => n.IngredienNumber == ingredientNumber).ToList();
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
    }
}
