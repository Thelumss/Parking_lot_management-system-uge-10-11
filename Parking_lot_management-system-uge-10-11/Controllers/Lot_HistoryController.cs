using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class Lot_HistoryController : Controller
    {
        private readonly ILot_HistoryRepostiory lot_HistoryRepostiory;

        public Lot_HistoryController(ILot_HistoryRepostiory lot_History)
        {
            this.lot_HistoryRepostiory = lot_History;
        }

        [HttpGet("/lot_History/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot_History>))]
        public IActionResult GetAllLot_History()
        {
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

        [HttpGet("/lot_History/License_plate{License_plate}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot_History>))]
        [ProducesResponseType(400)]
        public IActionResult getLot_HistorybyLicensePlate(string License_plate)
        {

            var lot_history = lot_HistoryRepostiory.GetLot_HistoryByLicence_plate(License_plate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot_history);
            }
        }

        [HttpPost("/lot_History/post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

            lot_HistoryRepostiory.CreateLot_History(lot_History);

            return Ok("Successfully created");

        }

        [HttpPut("/lot_History/put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLot_History([FromBody] Lot_History lot_History)
        {
            if (!lot_HistoryRepostiory.Lot_HistoryExist(lot_History.Lot_History_ID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            lot_HistoryRepostiory.UpdateLot_History(lot_History);
            return Ok("User Successfully Updated");
        }


        [HttpDelete("/lot_History/delete{Lot_History_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLot_History(int Lot_History_ID)
        {
            if (!lot_HistoryRepostiory.Lot_HistoryExist(Lot_History_ID))
            {
                return NotFound();
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
