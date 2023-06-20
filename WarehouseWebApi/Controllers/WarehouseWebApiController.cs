using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WarehouseWebApi.Dto_Models;
using WarehouseWebApi.Interfaces;
using WarehouseWebApi.Models;

namespace WarehouseWebApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class WarehouseWebApiController : ControllerBase
    {
        private readonly ISqlInterface _repo;
        private readonly IMapper _maper;

        public WarehouseWebApiController(ISqlInterface repo, IMapper mapper)
        {
            _repo = repo;
            _maper = mapper;
        }

        [HttpPost ("CreatePallet")]
        public async Task<ActionResult<DtoPalletModel>> CreatePalletAsync(DtoPalletModel dtoPallet)
        {
            if(dtoPallet != null)
            {
                try
                {
                    var check = DefaultMethods.CheckPalletNumber(dtoPallet.PalletNumber, _repo);
                    if(check != true)
                    {
                        var pallet = _maper.Map<PalletModel>(dtoPallet);
                        pallet.Status = "Free to use";
                        pallet.Localization = dtoPallet.Localization.Trim().ToLower();

                        await _repo.CreatePalletAsync(pallet);
                        await _repo.SaveChangesAsync();

                        var dtoReadPallet = _maper.Map<DtoPalletModel>(pallet);

                        return CreatedAtRoute(nameof(GetPalletByNumAsync), new { PalletNumber = pallet.PalletNumber }, dtoReadPallet);

                    }
                    return Ok("The pallet with the same number is already in Db!");
                }
                    catch (Exception ex)
                    {
                        return NotFound("Some error occured - " + ex.Message.ToString());

                    }
                
            }
            return NotFound("Please, entere correct values!");
        }

        [HttpGet("GetPalletByNum/{palletNumber}", Name = "GetPalletByNumAsync")]
        public async Task<ActionResult<DtoPalletModel>> GetPalletByNumAsync(long palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = await _repo.GetPalletByNumAsync(palletNumber);
                    return Ok(_maper.Map<DtoPalletModel>(pallet));
                }
                catch (Exception ex)
                {
                    return NotFound("Please, make sure the 'PalletNumber' value is correct " + ex.Message.ToString());
                }
            }
            return NotFound("Please, make sure you entered correct value!");
        }

        [HttpGet ("GetAllPallets")]
        public async Task<ActionResult<IEnumerable<DtoPalletModel>>> GetAllPalletsAsync()
        {
            try
            {
                var pallets = await _repo.GetPalletsAsync();

                return Ok(_maper.Map<IEnumerable<DtoPalletModel>>(pallets));
            }
            catch( Exception ex)
            {
                return NotFound("Some exception occured - " + ex.Message.ToString());
            }
        }

        [HttpPut ("UpdatePallet/{palletNumber}")]
        public async Task<ActionResult<DtoPalletModel>> UpdatePalletAsync(long palletNumber, [FromBody] DtoPalletModel dtoPallet)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = _maper.Map<PalletModel>(dtoPallet);
                    pallet.Status = _repo.GetPalletByNumAsync(palletNumber).Result.Status;

                    await _repo.UpdatePalletAsync(palletNumber, pallet);
                    await _repo.SaveChangesAsync();

                    return Ok(dtoPallet);
                }
                catch(Exception ex)
                {
                    return NotFound("Some error occured - " + ex.Message.ToString());
                }
            }
            return NotFound("Please, make sure you entered correct 'PalletNumber'");
        }

        [HttpPatch ("PartialUpdate")]
        public async Task<ActionResult<DtoPalletModel>> PartialUpdateAsync(long palletNumber, JsonPatchDocument<DtoPalletModel> patchPallet)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = await _repo.GetPalletByNumAsync(palletNumber);
                    var palletToPatch = _maper.Map<DtoPalletModel>(pallet);
                    patchPallet.ApplyTo(palletToPatch, ModelState);

                    if (!TryValidateModel(palletToPatch))
                    {
                        return ValidationProblem(ModelState);
                    }
                    _maper.Map(palletToPatch, pallet);
                    await _repo.UpdatePalletAsync(palletNumber, pallet);
                    await _repo.SaveChangesAsync();

                    return Ok(_maper.Map<DtoPalletModel>(pallet));
                }
                catch(Exception ex)
                {
                    return BadRequest("Some error occured " + ex.Message.ToString());
                }
            }

            return NotFound("Please, make sure you entered correct 'PalletNumber'");
        }

        [HttpDelete("DeletePallet/{palletNumber}")]
        public async Task<ActionResult> DeletePalletAsync(long palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = await _repo.GetPalletByNumAsync(palletNumber);
                    await _repo.DeletePalletAsync(pallet);
                    await _repo.SaveChangesAsync();

                    return Ok("Pallet has benn deleted from database!");
                }
                catch(Exception ex)
                {
                    return NotFound("Some error occured - " + ex.Message.ToString());
                }
            }
            return NotFound("Please, make sure you entered correct 'PalletNumber'");
        }

        [HttpPost("NewOrderCreating")]
        public async Task<ActionResult<DtoNewOrderModel>> NewOrderCreatingAsync([FromBody] string localizationName, int count, int igredientNumber)
        {
            if(localizationName != null && count != 0 && igredientNumber !=0)
            {
                if(!string.Equals(localizationName.Trim(), "string", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var newOrder = await _repo.CreateOrderAsync(localizationName, count, igredientNumber);

                        if (newOrder.Paletts.Count() > 0)
                        {
                            foreach (var item in newOrder.Paletts)
                            {
                                item.Status = "In process";
                                item.Localization = localizationName;
                                await _repo.UpdatePalletAsync(item.PalletNumber, item);
                            }

                            await _repo.SaveChangesAsync();
                            return Ok(_maper.Map<DtoNewOrderModel>(newOrder));
                        }
                        return NotFound("Please, make sure you entered correct values or may be the pallet has already been used!");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Some error occured - " + ex.Message.ToString());
                    }
                }
                return NotFound("Please, make sure you entered correct localization!");
            }
            return BadRequest("Make sure you entered all values!");
        }

        [HttpGet("CheckLocalization")]
        public async Task<ActionResult<IEnumerable<PalletModel>>> CheckLocalizationAsync(string enteredLocalization)
        {
            if(enteredLocalization != null)
            {
                var localization = enteredLocalization.Replace("\"", "").Trim().ToLower();
                try
                {
                    var list = new List<DtoPalletModel>();

                    foreach(var pallet in await _repo.LocalizationCheckAsync(localization))
                    {
                        list.Add(_maper.Map<DtoPalletModel>(pallet));
                    }
                
                    return Ok(list);

                }
                catch(Exception ex)
                {
                    return NotFound("Some error occured - " + ex.Message.ToString());
                }
            }
            return NotFound("Make sure you entered correct values!");

        }

    }
}
