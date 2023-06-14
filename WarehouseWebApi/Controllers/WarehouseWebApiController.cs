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
                    var pallet = _maper.Map<PalletModel>(dtoPallet);
                    await _repo.CreatePalletAsync(pallet);
                    await _repo.SaveChangesAsync();

                    var dtoReadPallet = _maper.Map<DtoPalletModel>(pallet);

                    return CreatedAtRoute(nameof(GetPalletByNumAsync), new { PalletNumber = pallet.PalletNumber }, dtoReadPallet);
                }
                catch(Exception ex)
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
                    await _repo.UpdatePalletAsync(palletNumber, _maper.Map<PalletModel>(dtoPallet));
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

    }
}
