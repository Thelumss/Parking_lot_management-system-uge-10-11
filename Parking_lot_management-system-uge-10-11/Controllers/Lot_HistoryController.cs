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
    public class Lot_HistoryController : Controller
    {
        private readonly ILotRepository lotRepository;
        private readonly IParking_Lot_structursRepository parking_Lot_StructursRepository;
        private readonly ILot_HistoryRepostiory lot_HistoryRepostiory;

        public Lot_HistoryController(ILotRepository lot, IParking_Lot_structursRepository parking_Lot_StructursRepository, ILot_HistoryRepostiory lot_HistoryRepostiory)
        {
            this.lotRepository = lot;
            this.parking_Lot_StructursRepository = parking_Lot_StructursRepository;
            this.lot_HistoryRepostiory = lot_HistoryRepostiory;
        }

        [HttpGet("/lot_History/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot_History>))]
        [Authorize]
        public IActionResult GetAllLot_History()
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot_history = lot_HistoryRepostiory.GetAllLot_Historys();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot_history);
            }
        }

        [HttpGet("/lot_History/OrganisationId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot_History>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult getLot_HistorybyLicensePlate()
        {



            var usersOrganisationId = User.FindFirst("OrganisationId")?.Value;
            var lot_history = lot_HistoryRepostiory.GetLot_HistoryByOrganisationId(int.Parse(usersOrganisationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot_history);
            }
        }


        [HttpGet("/lot_History/adminOrganisationId{OrganisationId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot_History>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult getadminLot_HistorybyLicensePlate(int OrganisationId)
        {

            var lot_history = lot_HistoryRepostiory.GetLot_HistoryByOrganisationId(OrganisationId);


            var usersOrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (OrganisationId != int.Parse(usersOrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot_history);
            }
        }

        [HttpPost("/lot_History/CreateLot_History")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult CreateLot_History([FromBody] Lot_History lot_History)
        {
            if (lot_History == null)
            {
                return BadRequest(ModelState);
            }

            if (lot_HistoryRepostiory.Lot_HistoryExist(lot_History.Lot_History_ID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lot_History.Lot_History_ID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }
            var lot = lotRepository.GetLotbyID(lot_History.Lot_ID);
            var building = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lot.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (building.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            lot_HistoryRepostiory.CreateLot_History(lot_History);

            return Ok("Successfully created");

        }

        [HttpPut("/lot_History/UpdateLot_History")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult UpdateLot_History([FromBody] Lot_History lot_History)
        {
            if (!lot_HistoryRepostiory.Lot_HistoryExist(lot_History.Lot_History_ID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            var lot = lotRepository.GetLotbyID(lot_History.Lot_ID);
            var building = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lot.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (building.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            lot_HistoryRepostiory.UpdateLot_History(lot_History);
            return Ok("User Successfully Updated");
        }


        [HttpDelete("/lot_History/delete{Lot_History_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteLot_History(int Lot_History_ID)
        {
            if (!lot_HistoryRepostiory.Lot_HistoryExist(Lot_History_ID))
            {
                return NotFound();
            }
            var lot = lotRepository.GetLotbyID(Lot_History_ID);
            var building = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lot.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (building.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            var userToDelete = lot_HistoryRepostiory.GetLot_HistoryByID(Lot_History_ID);

            if (!lot_HistoryRepostiory.DeleteLot_History(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }

            return NoContent();
        }
    }
}
