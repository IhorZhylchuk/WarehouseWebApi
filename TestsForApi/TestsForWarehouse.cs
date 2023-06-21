using Microsoft.EntityFrameworkCore;
using WarehouseWebApi.Models;

namespace TestsForApi
{
    public class TestsForWarehouse
    {
        public DbContextOptions<ApplicationDbContex> Options()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContex>()
                .UseInMemoryDatabase(databaseName: "Warehouse")
                .Options;
            return dbContextOptions;
        }

        public List<PalletModel> Pallets()
        {
            List<PalletModel> pallets = new List<PalletModel>() { 
                new PalletModel{ Id = 1, IngredienNumber = 44067256, Ilość = 100, Localization = "Warehouse",
                MaterialName = "Laminate Mlay", PalletNumber = 3331125678, Status = "Free to use"},
                new PalletModel{ Id = 2, IngredienNumber = 44067256, Ilość = 200, Localization = "Warehouse",
                MaterialName = "Laminate Mlay", PalletNumber = 3339925678, Status = "Free to use"},
                new PalletModel{ Id = 3, IngredienNumber = 44067332, Ilość = 400, Localization = "Warehouse",
                MaterialName = "Laminate LK", PalletNumber = 3390125678, Status = "Free to use"},
            };
            return pallets;
        }
        [Fact]
        public async Task Test_For_Getting_All_Pallets()
        {
            using (var db = new ApplicationDbContex(Options()))
            {
                db.Pallets.AddRange(Pallets());
                db.SaveChanges();

                SqlRepo _sqlRepo = new SqlRepo(db);
                var returnedPallets = await _sqlRepo.GetPalletsAsync();

                Assert.NotNull(returnedPallets);
                Assert.IsAssignableFrom<List<PalletModel>>(returnedPallets);

                db.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Test_For_Adding_New_Pallet()
        {
            using (var db = new ApplicationDbContex(Options()))
            {
                db.Pallets.AddRange(Pallets());
                db.SaveChanges();

                var countBeforAdding = db.Pallets.Count();

                var pallet = new PalletModel
                {
                    Id = 4,
                    IngredienNumber = 44067256,
                    Ilość = 100,
                    Localization = "Warehouse",
                    MaterialName = "Laminate Mlay",
                    PalletNumber = 3331125678,
                    Status = "Free to use"
                };


                SqlRepo _sqlRepo = new SqlRepo(db);
                await _sqlRepo.CreatePalletAsync(pallet);
                db.SaveChanges();
                var countAfter = db.Pallets.Count();

                Assert.NotEqual(countBeforAdding, countAfter);
                Assert.NotEmpty(db.Pallets.ToList());
                Assert.False(countBeforAdding > countAfter);
                Assert.True(countAfter > countBeforAdding);

                db.Database.EnsureDeleted();
            }

        }

        [Fact]
        public async Task Test_For_Deleting_Pallet()
        {
            using (var db = new ApplicationDbContex(Options()))
            {
                db.Pallets.AddRange(Pallets());
                db.SaveChanges();

                var countBeforDeleting = db.Pallets.Count();

                SqlRepo _sqlRepo = new SqlRepo(db);
                var pallet = await _sqlRepo.GetPalletByNumAsync(3331125678);
                await _sqlRepo.DeletePalletAsync(pallet);
                db.SaveChanges();
                var countAfterDeleting = db.Pallets.Count();

                Assert.NotEqual(countBeforDeleting, countAfterDeleting);
                Assert.NotEqual(pallet.Id, db.Pallets.First().Id);

                db.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Test_For_Update_Pallet()
        {
            using (var db = new ApplicationDbContex(Options()))
            {
                db.Pallets.AddRange(Pallets());
                db.SaveChanges();

                SqlRepo _sqlRepo = new SqlRepo(db);
                var palletBeforeUpdate = await _sqlRepo.GetPalletByNumAsync(3331125678);
                palletBeforeUpdate.Status = "In process";
                palletBeforeUpdate.Ilość = 10000;
                palletBeforeUpdate.IngredienNumber = 44021348;
                await _sqlRepo.SaveChangesAsync();

                var palletAfterUpdate = await _sqlRepo.GetPalletByNumAsync(3331125678);

                Assert.NotNull(palletAfterUpdate);
                Assert.NotEqual(Pallets().First().Status, palletBeforeUpdate.Status);
                Assert.NotEqual(Pallets().First().Ilość, palletBeforeUpdate.Ilość);
                Assert.NotEqual(Pallets().First().IngredienNumber, palletBeforeUpdate.IngredienNumber);

                db.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Test_For_Creating_Orders()
        {
            using (var db = new ApplicationDbContex(Options()))
            {
                db.Pallets.AddRange(Pallets());
                db.SaveChanges();

                int ingredientNumber = 44067256;
                string localization = "Machine nr. 1";
                int count = 100;

                SqlRepo _sqlRepo = new SqlRepo(db);
                var newOrder = await _sqlRepo.CreateOrderAsync(localization, count, ingredientNumber);
                await _sqlRepo.SaveChangesAsync();

                var localizationCheck = await _sqlRepo.LocalizationCheckAsync(localization);
                var orders = db.Orders.Select(o => o).First();

                Assert.NotNull(orders);
                Assert.IsAssignableFrom<NewOrder>(orders);
                Assert.NotNull(localizationCheck);
                Assert.IsAssignableFrom<IEnumerable<PalletModel>>(localizationCheck);

            }
        }

    }
}