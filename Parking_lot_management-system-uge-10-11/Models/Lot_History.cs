using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Lot_History
    {
        [Key]
        public int Lot_History_ID { get; set; }

        public string License_PLate_Numbers { get; set; }

        public long? Entry_time { get; set; }
        public long? Exit_time { get; set; }

        public float? Charged { get; set; }

        public int Lot_ID { get; set; }
        [ForeignKey("Lot_ID")]
        [JsonIgnore]
        public virtual Lot? Lot { get; set; }
    }
}
