using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;
using System;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository user)
        {
            this.userRepository = user;
        }

        [HttpGet("/User/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [Authorize]
        public IActionResult GetAllLotTypes()
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

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
        [HttpGet("/user/adminByOrganisation{OrganisationID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetUserbyOrganisationAdmin(int OrganisationID)
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

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

        [HttpGet("/user/byOrganisation{OrganisationID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetUserbyOrganisation(int OrganisationID)
        {
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (OrganisationID != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

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

        [HttpGet("/user/adminByType{TypeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetUserbyTypeAdmin(int TypeID)
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

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

        [HttpGet("/user/byType{TypeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetUserbyType(int TypeID)
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 == TypeID && 1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            var users = userRepository.GetUsersByTypeIDAndOrganisationId(TypeID, int.Parse(UserTypeID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpPut("/User/UpdateUserTypes")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult UpdateUserTypes([FromBody] Users users)
        {
            if (!userRepository.UsersExist(users.UserID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }
            var userToUpdate = userRepository.GetUsersbyID(users.UserID);
            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (userToUpdate.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            userRepository.UpdateUsers(users);
            return Ok("User Successfully Updated");
        }

        [HttpDelete("/User/delete{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteUsers(int UserId)
        {

            if (!userRepository.UsersExist(UserId))
            {
                return NotFound();
            }

            var userToDelete = userRepository.GetUsersbyID(UserId);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if ( userToDelete.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            if (!userRepository.DeleteUsers(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User");
            }

            return NoContent();
        }
    }
}
