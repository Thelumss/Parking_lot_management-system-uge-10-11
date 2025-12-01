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
    public class Parking_Lot_structursController : Controller
    {
        private readonly IParking_Lot_structursRepository parking_Lot_StructursRepository;

        public Parking_Lot_structursController(IParking_Lot_structursRepository parking)
        {
            this.parking_Lot_StructursRepository = parking;
        }

        [HttpGet("/parking_Lot_Structur/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Parking_Lot_Structur>))]
        [Authorize]
        public IActionResult GetAllParking_Lot_Structur()
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

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

        [HttpGet("/parking_Lot_Structur/AdminbyOrganisation{OrganisationID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Parking_Lot_Structur>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetParking_Lot_StructurbyOrganisation(int OrganisationID)
        {
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (OrganisationID != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

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

        [HttpGet("/parking_Lot_Structur/byOrganisation")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Parking_Lot_Structur>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetParking_Lot_StructurbyOrganisation()
        {
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            var parking_Lot_Structur = parking_Lot_StructursRepository.Getparking_Lot_StructurByOrganisationId(int.Parse(OrganisationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(parking_Lot_Structur);
            }
        }

        [HttpGet("/parking_Lot_Structur/partingid{partingid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Parking_Lot_Structur>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetParking_Lot_Structurbypartingid(int partingid)
        {
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            var parking_Lot_Structur = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(partingid);
            int parking_structurID = parking_Lot_Structur.OrganisationId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (parking_structurID != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }
            else
            {
                return Ok(parking_Lot_Structur);
            }
        }

        [HttpPost("/parking_Lot_Structur/CreateParking_Lot_Structur")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult CreateParking_Lot_Structur([FromBody] Parking_Lot_Structur parking)
        {
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

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

            parking.OrganisationId = int.Parse(OrganisationId);

            parking_Lot_StructursRepository.CreateParking_Lot_Structur(parking);

            return Ok("Successfully created");

        }

        [HttpPut("/parking_Lot_Structur/UpdateParking_Lot_Structur")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult UpdateParking_Lot_Structur([FromBody] Parking_Lot_Structur parking)
        {
            if (!parking_Lot_StructursRepository.parking_Lot_StructurExist(parking.Parking_lot_Structur_ID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }
            var parking_Lot_StructurToUpdate = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking.Parking_lot_Structur_ID);
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parking_Lot_StructurToUpdate.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            parking_Lot_StructursRepository.Updateparking_Lot_Structur(parking_Lot_StructurToUpdate, parking);
            return Ok("parking Lot Structur Successfully Updated");
        }

        [HttpDelete("/parking_Lot_Structur/delete{parking_Lot_StructurID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteParking_Lot_Structur(int parking_Lot_StructurID)
        {
            if (!parking_Lot_StructursRepository.parking_Lot_StructurExist(parking_Lot_StructurID))
            {
                return NotFound();
            }

            var parking_Lot_StructurToDelete = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parking_Lot_StructurToDelete.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            if (!parking_Lot_StructursRepository.Deleteparking_Lot_Structur(parking_Lot_StructurToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }

            return NoContent();
        }
    }
}
