namespace Parking_lot_management_system_uge_10_11.DTO
{
    public class LotHistoryDto
    {
        public string License_PLate_Numbers { get; set; }
        public long? Entry_time { get; set; }
        public long? Exit_time { get; set; }
        public float? Charged { get; set; }
        public bool Active { get; set; } = true;

        public string Parking_Lot_Structur_Name { get; set; }
        public string Lot_Type { get; set; }
    }
}
