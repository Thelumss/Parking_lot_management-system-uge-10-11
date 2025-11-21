using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Controllers
{

    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Authorize]
    public class UserstypesController : Controller
    {
        private readonly IUserTypesRepository userTypesRepository;

        public UserstypesController(IUserTypesRepository userTypes)
        {
            this.userTypesRepository = userTypes;
        }

        [HttpGet("/UserTypes/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User_Types>))]
        [Authorize]
        public IActionResult GetAllUser_types()
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            var user_types = userTypesRepository.GetUser_Types();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(user_types);
            }

        }

        [HttpPost("/UserTypes/CreateUserTypes")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult CreateUserTypes([FromBody] User_Types user_Types)
        {
            if (user_Types == null) 
            {
                return BadRequest(ModelState);
            }

            if (userTypesRepository.UserTypesExist(user_Types.User_TypesID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user_Types.User_TypesID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            userTypesRepository.CreateUserTypes(user_Types);

            return Ok("Successfully created");
        }

        [HttpPut("/UserTypes/UpdateUsertypes")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult UpdateUsertypes([FromBody] User_Types user_TypesUpdate)
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 == user_TypesUpdate.User_TypesID && 1!= int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            if (!userTypesRepository.UserTypesExist(user_TypesUpdate.User_TypesID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            userTypesRepository.UpdateUserTypes(user_TypesUpdate);
            return Ok("User types Successfully Updated");
        }

        [HttpDelete("/UserTypes/delete{Usertypesid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteUsertypes(int Usertypesid)
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 == Usertypesid && 1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            if (!userTypesRepository.UserTypesExist(Usertypesid))
            {
                return NotFound();
            }

            var usertypesToDelete = userTypesRepository.GetUserTypes(Usertypesid);

            if (!userTypesRepository.DeleteUserTypes(usertypesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting User types");
            }

            return NoContent();
        }
    }
}
