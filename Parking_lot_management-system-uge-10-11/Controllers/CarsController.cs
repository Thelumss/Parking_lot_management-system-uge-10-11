using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class CarsController : Controller
    {

        private readonly ILotRepository lotRepository;
        private readonly IParking_Lot_structursRepository parking_Lot_StructursRepository;
        private readonly ILot_HistoryRepostiory lot_HistoryRepostiory;

        public CarsController(ILotRepository lot, IParking_Lot_structursRepository parking_Lot_StructursRepository, ILot_HistoryRepostiory lot_HistoryRepostiory)
        {
            this.lotRepository = lot;
            this.parking_Lot_StructursRepository = parking_Lot_StructursRepository;
            this.lot_HistoryRepostiory = lot_HistoryRepostiory;
        }

        [HttpPut("/Cars/CarsIn/{Parking_lot_Structur_ID}/{License_plate}/{lottype}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CarsIn(int Parking_lot_Structur_ID, string License_plate, int lottype)
        {
            if (Parking_lot_Structur_ID == 0 || License_plate == null || lottype == 0)
            {
                return BadRequest(ModelState);
            }

            if (!parking_Lot_StructursRepository.parking_Lot_StructurExist(Parking_lot_Structur_ID))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carLot = lotRepository.GetFirstBestLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(Parking_lot_Structur_ID, lottype, false);

            if (carLot == null)
            {
                ModelState.AddModelError("", "No spot available");
                return StatusCode(500, ModelState);
            }

            Parking_Lot_Structur Parkinglot = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(Parking_lot_Structur_ID);
            Parkinglot.Total_Available_Lots--;
            Parkinglot.Total_Occupied_Lots++;
            parking_Lot_StructursRepository.Updateparking_Lot_Structur(Parkinglot, Parkinglot);

            carLot.Occupied_Status = true;
            lotRepository.UpdateLots(carLot);
            Lot_History newLot_History = new Lot_History
            {
                Lot_ID = carLot.LotID,
                License_PLate_Numbers = License_plate,
                Entry_time = DateTimeOffset.Now.ToUnixTimeMilliseconds()

            };

            lot_HistoryRepostiory.CreateLot_History(newLot_History);

            return Ok(new { LotName = carLot.LotName, Message = "Car got a lot" });

        }

        [HttpPut("/Cars/CarsOut/{Parking_lot_Structur_ID}/{License_plate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CarsOut(int Parking_lot_Structur_ID, string License_plate)
        {
            if (Parking_lot_Structur_ID == 0 || License_plate == null)
            {
                return BadRequest(ModelState);
            }

            if (!parking_Lot_StructursRepository.parking_Lot_StructurExist(Parking_lot_Structur_ID))
            {
                return BadRequest(ModelState);
            }

            var carsLot = lot_HistoryRepostiory.GetLot_HistoryByLicence_plateAndActive(License_plate, true);
            if(carsLot == null)
            {
                return NotFound();
            }
            var lot = lotRepository.GetLotbyID(carsLot.Lot_ID);
            var parkingStrucur = parking_Lot_StructursRepository.Getparking_Lot_StructurByID(lot.Structur_ID);

            if (parkingStrucur.Parking_lot_Structur_ID != Parking_lot_Structur_ID)
            {
                return BadRequest(ModelState);
            }

            parkingStrucur.Total_Available_Lots++;
            parkingStrucur.Total_Occupied_Lots--;
            parking_Lot_StructursRepository.Updateparking_Lot_Structur(parkingStrucur, parkingStrucur);

            lot.Occupied_Status = false;
            lotRepository.UpdateLots(lot);

            carsLot.Exit_time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            DateTimeOffset Entry_time = DateTimeOffset.FromUnixTimeMilliseconds((long)carsLot.Entry_time);
            DateTimeOffset Exit_time = DateTimeOffset.FromUnixTimeMilliseconds((long)carsLot.Exit_time);

            TimeSpan difference = Exit_time - Entry_time;

            double hoursdifference = difference.TotalHours;

            double price = parkingStrucur.BasePrice*hoursdifference;

            carsLot.Charged = (float?)price;
            carsLot.active = false;

            lot_HistoryRepostiory.UpdateLot_History(carsLot);



            return Ok(new { price, Message = "Car got a out and heres the price" });

        }
    }
}
