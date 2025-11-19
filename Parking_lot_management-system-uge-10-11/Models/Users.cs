using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class Users
    {

        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int UserTypeID { get; set; }

        [ForeignKey("UserTypeID")]
        [JsonIgnore]
        public virtual User_Types? UserType { get; set; }
        
        
        public int OrganisationId { get; set; }
        
        [ForeignKey("OrganisationId")]
        [JsonIgnore]
        public virtual Organisation? Organisation { get; set; }

    }
}
