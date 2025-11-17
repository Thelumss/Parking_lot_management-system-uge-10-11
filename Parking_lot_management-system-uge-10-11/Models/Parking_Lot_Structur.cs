using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Parking_Lot_Structur
    {
        public int Parking_lot_Structur_id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public int Total_Available_Lots { get; set; }

        public int Total_Occupied_Lots { get; set; }


        public int OrganisationId { get; set; }

        [ForeignKey("OrganisationId")]
        public virtual Organisation Organisation { get; set; }

    }
}
