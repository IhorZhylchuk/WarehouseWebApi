using AutoMapper;
using WarehouseWebApi.Dto_Models;
using WarehouseWebApi.Models;

namespace WarehouseWebApi.Profiles
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile() 
        {
            CreateMap<PalletModel, DtoPalletModel>();
            CreateMap<DtoPalletModel, PalletModel>();
            CreateMap<DefaultModelForNewOrder, DtoNewOrderModel>();
        }
    }
}
