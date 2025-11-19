using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository user)
        {
            this.userRepository = user;
        }

        [HttpGet("/User/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public IActionResult GetAllLotTypes()
        {
            var user = userRepository.GetAllUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet("/user/byOrganisation{OrganisationID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserbyOrganisation(int OrganisationID)
        {

            var users = userRepository.GetUsersByOrganisationId(OrganisationID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpGet("/user/byType{TypeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserbyType(int TypeID)
        {

            var users = userRepository.GetUsersByTypeID(TypeID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(users);
            }
        }


        [HttpPost("/User/CreateUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateUser([FromBody] Users users)
        {
            if (users == null)
            {
                return BadRequest(ModelState);
            }

            if (userRepository.UsersExist(users.UserID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (users.UserID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            userRepository.CreateUsers(users);

            return Ok("Successfully created");

        }

        [HttpPut("/User/UpdateUserTypes")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserTypes([FromBody] Users users)
        {
            if (!userRepository.UsersExist(users.UserID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            userRepository.UpdateUsers(users);
            return Ok("User Successfully Updated");
        }

        [HttpDelete("/User/delete{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUsers(int UserId)
        {
            if (!userRepository.UsersExist(UserId))
            {
                return NotFound();
            }

            var userToDelete = userRepository.GetUsersbyID(UserId);

            if (!userRepository.DeleteUsers(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }

            return NoContent();
        }
    }
}
