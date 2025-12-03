using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Parking_lot_management_system_uge_10_11.DTO;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Authorize]
    public class LotController : Controller
    {
        private readonly ILotRepository lotRepository;
        private readonly IParking_Lot_structursRepository parking_Lot_StructursRepository;

        public LotController(ILotRepository lot, IParking_Lot_structursRepository parking_Lot_StructursRepository)
        {
            this.lotRepository = lot;
            this.parking_Lot_StructursRepository = parking_Lot_StructursRepository;
        }

        [HttpGet("/Lot/All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [Authorize]
        public IActionResult GetAllLot()
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetAllLots();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("/Lot/byparking_Lot_Structur{parking_Lot_StructurId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReadlotDTO>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetLotsbyOrganisation(int parking_Lot_StructurId)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurId);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetLotByParking_Lot_StructurId(parking_Lot_StructurId);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("/Lot/byLotType{LotTypeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetLotsbyLotType(int LotTypeId)
        {
            var UserTypeID = User.FindFirst("UserTypeID")?.Value;

            if (1 != int.Parse(UserTypeID))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetLotByparking_Lot_StructurId(LotTypeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("Lot/GetLotsbyLotTypeAndparking_Lot_Structur/{lotTypeId}/{parking_Lot_StructurId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetLotsbyLotTypeAndparking_Lot_Structur(int lotTypeId, int parking_Lot_StructurId)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurId);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetLotByparking_Lot_StructurIdAndLottypeID(parking_Lot_StructurId, lotTypeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("Lot/GetLotsbyLotTypeAndparking_Lot_StructurAndOccupied_Status/{lotTypeId}/{parking_Lot_StructurId}/{occupied_Status}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetLotsbyLotTypeAndparking_Lot_StructurAndOccupied_Status(int lotTypeId, int parking_Lot_StructurId, bool occupied_Status)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurId);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(parking_Lot_StructurId, lotTypeId, occupied_Status);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("Lot/GetLotByparking_Lot_StructurIdAndLotName/{parking_Lot_StructurId}/{lotName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetLotByparking_Lot_StructurIdAndLotName(int parking_Lot_StructurId, string lotName)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurId);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetLotByparking_Lot_StructurIdAndLotName(parking_Lot_StructurId, lotName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }

        [HttpGet("Lot/GetLotByparking_Lot_StructurIdAndLottypeIDAndLotName/{lotTypeId}/{parking_Lot_StructurId}/{occupied_Status}/{lotName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lot>))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetLotByparking_Lot_StructurIdAndLottypeIDAndLotName(int lotTypeId, int parking_Lot_StructurId, string lotName)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(parking_Lot_StructurId);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            var lot = lotRepository.GetLotByparking_Lot_StructurIdAndLottypeIDAndLotName(parking_Lot_StructurId, lotTypeId, lotName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(lot);
            }
        }
        [HttpPost("/Lot/CreateLot")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult CreateLot([FromBody] Lot lot)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lot.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            if (lot == null)
            {
                return BadRequest(ModelState);
            }

            if (lotRepository.LotsExist(lot.LotID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lot.LotID != 0)
            {
                ModelState.AddModelError("", "Can't give id");
            }

            lotRepository.CreateLots(lot);

            return Created();

        }

        [HttpPost("/Lot/CreateLots")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult CreateLots([FromBody] CreateLotsDTO lotsDTO)
        {
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lotsDTO.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //for (int j = 0; j < lotsDTO.Length; j++)
            //{
                int preExistingAmount = lotRepository.CountLotsByStructureIdAndStartingLetter(parkinglot.Parking_lot_Structur_ID, lotsDTO.Areaname);
                for (int i = 0+preExistingAmount; i < lotsDTO.amount+preExistingAmount; i++)
                {
                    var temp = i+1;
                    Lot lot = new Lot
                    {
                        LotID = 0,
                        LotName = lotsDTO.Areaname+temp.ToString(),
                        Occupied_Status = false,
                        Structur_ID = lotsDTO.Structur_ID,
                        Lot_types_ID = lotsDTO.lottypes,
                    };

                    lotRepository.CreateLots(lot);
                    parkinglot.Total_Available_Lots++;

                }
            //}

            parking_Lot_StructursRepository.Updateparking_Lot_Structur(parkinglot, parkinglot);



            return Created();

        }

        [HttpPut("/Lot/UpdateLot")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult UpdateLot([FromBody] Lot lot)
        {

            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lot.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }


            if (!lotRepository.LotsExist(lot.LotID))
            {
                ModelState.AddModelError("", "id did not exist");
                return StatusCode(500, ModelState);
            }

            lotRepository.UpdateLots(lot);
            return Created();
        }

        [HttpDelete("/Lot/delete{LotID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteLot(int LotID)
        {

            if (!lotRepository.LotsExist(LotID))
            {
                return NotFound();
            }

            var userToDelete = lotRepository.GetLotbyID(LotID);
            var parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(userToDelete.Structur_ID);

            var OrganisationId = User.FindFirst("OrganisationId")?.Value;

            if (parkinglot.OrganisationId != int.Parse(OrganisationId))
            {
                return StatusCode(403, "Permission denied");
            }

            if (!lotRepository.DeleteLots(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Lot");
            }

            return NoContent();
        }
    }
}
