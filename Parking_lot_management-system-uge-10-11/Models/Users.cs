using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Users
    {

        public int Userid { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int UserTypeID { get; set; }

        [ForeignKey("UserTypeID")]
        public virtual User_Types UserType { get; set; }
        
        
        public int OrganisationId { get; set; }
        
        [ForeignKey("OrganisationId")]
        public virtual Organisation Organisation { get; set; }

    }
}
