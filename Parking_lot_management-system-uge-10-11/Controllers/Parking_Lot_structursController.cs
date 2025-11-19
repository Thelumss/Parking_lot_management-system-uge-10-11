using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class Parking_Lot_structursController : Controller
    {
        private readonly IParking_Lot_structursRepository parking_Lot_StructursRepository;

        public Parking_Lot_structursController(IParking_Lot_structursRepository parking)
        {
            this.parking_Lot_StructursRepository = parking;
        }

        [HttpGet("/parking_Lot_Structur/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Parking_Lot_Structur>))]
        public IActionResult GetAllLotTypes()
        {
            var parking_Lot_Structur = parking_Lot_StructursRepository.GetAllparking_Lot_Structur();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(parking_Lot_Structur);
            }
        }

        [HttpGet("/parking_Lot_Structur/byOrganisation{OrganisationID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        public IActionResult getCerealbyOrganisation(int OrganisationID)
        {

            var parking_Lot_Structur = parking_Lot_StructursRepository.Getparking_Lot_StructurByOrganisationId(OrganisationID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(parking_Lot_Structur);
            }
        }

        [HttpPost("/parking_Lot_Structur/post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateUser([FromBody] Parking_Lot_Structur parking)
        {
            if (parking == null)
            {
                return BadRequest(ModelState);
            }

            if (parking_Lot_StructursRepository.parking_Lot_StructurExist(parking.Parking_lot_Structur_ID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (parking.Parking_lot_Structur_ID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            parking_Lot_StructursRepository.CreateParking_Lot_Structur(parking);

            return Ok("Successfully created");

        }

        [HttpPut("/parking_Lot_Structur/put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserTypes([FromBody] Parking_Lot_Structur parking)
        {
            if (!parking_Lot_StructursRepository.parking_Lot_StructurExist(parking.Parking_lot_Structur_ID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            parking_Lot_StructursRepository.Updateparking_Lot_Structur(parking);
            return Ok("parking Lot Structur Successfully Updated");
        }

        [HttpDelete("/parking_Lot_Structur/delete{parking_Lot_StructurID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLottypes(int parking_Lot_StructurID)
        {
            if (!parking_Lot_StructursRepository.parking_Lot_StructurExist(parking_Lot_StructurID))
            {
                return NotFound();
            }

            var parking_Lot_StructurToDelete = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurID);

            if (!parking_Lot_StructursRepository.Deleteparking_Lot_Structur(parking_Lot_StructurToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }

            return NoContent();
        }
    }
}
