using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Authorize]
    public class LotTypesController : Controller
    {
        private readonly ILotTypesRepository lotTypesRepository;
        public LotTypesController(ILotTypesRepository lotTypes)
        {
            this.lotTypesRepository = lotTypes;
        }

        [HttpGet("/LotTypes/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot_types>))]
        [Authorize]
        public IActionResult GetAllLotTypes()
        {
            var lot_types = lotTypesRepository.GetAllLot_Types();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot_types);
            }
        }

        [HttpPost("/Lottypes/CreateLotTypes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult CreateLotTypes([FromBody] Lot_types lot_Types)
        {
            if(lot_Types == null)
            {
                return BadRequest(ModelState);
            }

            if(lotTypesRepository.LotTypesExist(lot_Types.Lot_typesID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lot_Types.Lot_typesID!= 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            lotTypesRepository.CreateLotTypes(lot_Types);

            return Ok("Successfully created");

        }

        [HttpPut("/Lottypes/UpdateUserTypes")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult UpdateUserTypes([FromBody] Lot_types lot_Types)
        {
            if (!lotTypesRepository.LotTypesExist(lot_Types.Lot_typesID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            lotTypesRepository.UpdateLotTypes(lot_Types);
            return Ok("Lot types Successfully Updated");
        }

        [HttpDelete("/Lottypes/delete{lottypesid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteLottypes(int lottypesid)
        {
            if (!lotTypesRepository.LotTypesExist(lottypesid))
            {
                return NotFound();
            }

            var lottypesToDelete = lotTypesRepository.GetLotTypes(lottypesid);

            if (!lotTypesRepository.DeleteLotTypes(lottypesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Lot types");
            }

            return NoContent();
        }
    }
}
