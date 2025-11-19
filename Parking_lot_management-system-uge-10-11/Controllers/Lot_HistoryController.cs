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
        public IActionResult GetAllLotTypes()
        {
            var lot_history = lot_HistoryRepostiory.GetAllUsers();

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
        public IActionResult getCerealbyOrganisation(string License_plate)
        {

            var lot_history = lot_HistoryRepostiory.GetLotByparking_Lot_StructurIdAndLottypeID(License_plate);

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
        public IActionResult CreateUser([FromBody] Lot_History lot_History)
        {
            if (lot_History == null)
            {
                return BadRequest(ModelState);
            }

            if (lot_HistoryRepostiory.UsersExist(lot_History.Lot_History_ID))
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

            lot_HistoryRepostiory.CreateUsers(lot_History);

            return Ok("Successfully created");

        }

        [HttpPut("/lot_History/put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserTypes([FromBody] Lot_History lot_History)
        {
            if (!lot_HistoryRepostiory.UsersExist(lot_History.Lot_History_ID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            lot_HistoryRepostiory.UpdateUsers(lot_History);
            return Ok("User Successfully Updated");
        }


        [HttpDelete("/lot_History/delete{Lot_History_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLottypes(int Lot_History_ID)
        {
            if (!lot_HistoryRepostiory.UsersExist(Lot_History_ID))
            {
                return NotFound();
            }

            var userToDelete = lot_HistoryRepostiory.GetUsersbyID(Lot_History_ID);

            if (!lot_HistoryRepostiory.DeleteUsers(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }

            return NoContent();
        }
    }
}
