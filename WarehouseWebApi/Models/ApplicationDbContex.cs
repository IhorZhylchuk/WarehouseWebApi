using Microsoft.EntityFrameworkCore;

namespace WarehouseWebApi.Models
{
    public class ApplicationDbContex:DbContext
    {
        public DbSet<PalletModel> Pallets { get; set; } 
        public DbSet<MachineModel> Machines { get; set; }
        public DbSet<NewOrder> Orders { get; set; }
        public ApplicationDbContex(DbContextOptions<ApplicationDbContex> dbContext) : base(dbContext)
        {

        }
    }
}
