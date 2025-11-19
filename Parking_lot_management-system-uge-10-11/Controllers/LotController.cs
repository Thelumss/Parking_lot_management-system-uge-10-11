using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class LotController : Controller
    {
        private readonly ILotRepository lotRepository;

        public LotController(ILotRepository lot)
        {
            this.lotRepository = lot;
        }

        [HttpGet("/Lot/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        public IActionResult GetAllLotTypes()
        {
            var lot = lotRepository.GetAllLots();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("/Lot/byparking_Lot_Structur{parking_Lot_StructurId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        public IActionResult GetLotsbyOrganisation(int parking_Lot_StructurId)
        {

            var lot = lotRepository.GetLotByparking_Lot_StructurId(parking_Lot_StructurId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("/Lot/byLotType{LotTypeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        public IActionResult GetLotsbyLotType(int LotTypeId)
        {

            var lot = lotRepository.GetLotByparking_Lot_StructurId(LotTypeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("Lot/byLotTypeAndbyparking_Lot_Structur/{lotTypeId}/{parking_Lot_StructurId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        public IActionResult GetLotsbyLotType(int lotTypeId, int parking_Lot_StructurId)
        {

            var lot = lotRepository.GetLotByparking_Lot_StructurIdAndLottypeID(parking_Lot_StructurId, lotTypeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("Lot/byLotTypeAndbyparking_Lot_Structur/{lotTypeId}/{parking_Lot_StructurId}/{occupied_Status}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        public IActionResult GetLotsbyLotType(int lotTypeId, int parking_Lot_StructurId,bool occupied_Status)
        {

            var lot = lotRepository.GetLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(parking_Lot_StructurId, lotTypeId, occupied_Status);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpPost("/Lot/post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateUser([FromBody] Lot lot)
        {
            if (lot == null)
            {
                return BadRequest(ModelState);
            }

            if (lotRepository.LotsExist(lot.LotID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lot.LotID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            lotRepository.CreateLots(lot);

            return Ok("Successfully created");

        }

        [HttpPut("/Lot/put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserTypes([FromBody] Lot lot)
        {
            if (!lotRepository.LotsExist(lot.LotID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            lotRepository.UpdateLots(lot);
            return Ok("Lot Successfully Updated");
        }

        [HttpDelete("/Lot/delete{LotID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLottypes(int LotID)
        {
            if (!lotRepository.LotsExist(LotID))
            {
                return NotFound();
            }

            var userToDelete = lotRepository.GetLotbyID(LotID);

            if (!lotRepository.DeleteLots(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Lot");
            }

            return NoContent();
        }
    }
}
