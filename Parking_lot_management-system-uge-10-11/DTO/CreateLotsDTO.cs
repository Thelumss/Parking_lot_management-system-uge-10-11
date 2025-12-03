using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.DTO
{
    public class CreateLotsDTO
    {
        public string Areaname { get; set; }
        public int amount { get; set; }

        public int Structur_ID { get; set; }
        public int lottypes { get; set; }

    }
}
