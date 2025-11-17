using System.ComponentModel.DataAnnotations;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Lot_types
    {
        [Key]
        public int Lot_typesID{ get; set; }

        public float Price_Multiplier { get; set; }

        public string Type { get; set; }
    }
}
