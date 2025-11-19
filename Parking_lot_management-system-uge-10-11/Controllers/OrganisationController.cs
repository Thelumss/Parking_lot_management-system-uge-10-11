using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class OrganisationController : Controller
    {
        private readonly IOrganisationRepository organisationRepository;
        public OrganisationController(IOrganisationRepository organisation)
        {
            this.organisationRepository = organisation;
        }

        [HttpGet("/Organisation/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Organisation>))]
        public IActionResult GetAllOrganisation()
        {
            var organisation = organisationRepository.GetAllOrganisation();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(organisation);
            }
        }

        [HttpPost("/Organisation/post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateOrganisations([FromBody] Organisation organisation)
        {
            if (organisation == null)
            {
                return BadRequest(ModelState);
            }

            if (organisationRepository.OrganisationExist(organisation.OrganisationID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (organisation.OrganisationID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            organisationRepository.CreateOrganisation(organisation);

            return Ok("Successfully created");
        }

        [HttpPut("/Organisation/put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrganisation([FromBody] Organisation organisation)
        {
            if (!organisationRepository.OrganisationExist(organisation.OrganisationID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            organisationRepository.UpdateOrganisation(organisation);
            return Ok("organisation Successfully Updated");
        }

        [HttpDelete("/Organisation/delete{Organisationid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrganisation(int organisationid)
        {
            if (!organisationRepository.OrganisationExist(organisationid))
            {
                return NotFound();
            }

            var organisationToDelete = organisationRepository.GetOrganisation(organisationid);

            if (!organisationRepository.DeleteOrganisation(organisationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting organisation");
            }

            return NoContent();
        }
    }
}
