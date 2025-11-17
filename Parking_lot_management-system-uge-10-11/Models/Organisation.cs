using System.ComponentModel.DataAnnotations;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Organisation
    {
        [Key]
        public int OrganisationID { get; set; }

        public string OrganisationName { get; set; }
    }
}
