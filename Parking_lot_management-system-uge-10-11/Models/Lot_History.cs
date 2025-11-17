using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Lot_History
    {
        [Key]
        public int Lot_History_ID { get; set; }

        public string License_PLate_Numbers { get; set; }

        public DateTime? Entry_time { get; set; }
        public DateTime? Exit_time { get; set; }

        public float? Charged { get; set; }

        public int Lot_ID { get; set; }
        [ForeignKey("Lot_ID")]
        public virtual Lot Lot { get; set; }
    }
}
