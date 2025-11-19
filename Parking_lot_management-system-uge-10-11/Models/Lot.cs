using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Lot
    {
        [Key]
        public int LotID { get; set; }
        public string LotName { get; set; }
        public bool Occupied_Status { get; set; }

        public int Structur_ID { get; set; }
        [ForeignKey("Structur_ID")]
        [JsonIgnore]
        public virtual Parking_Lot_Structur? Parking_Lot_Structur { get; set; }

        public int Lot_types_ID { get; set; }
        [ForeignKey("Lot_types_ID")]
        [JsonIgnore]
        public virtual Lot_types? Lot_types { get; set; }
    }
}
