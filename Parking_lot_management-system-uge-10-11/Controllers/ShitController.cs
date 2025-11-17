using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("API/shit")]
    [Microsoft.AspNetCore.Mvc.Controller]
    public class ShitController : Controller
    {
        [HttpGet("/all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public string GetUser()
        {

            return "test";
        }

        [HttpGet("/some")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        public string GetUsersome()
        {

            return "shit";
        }
    }
}
