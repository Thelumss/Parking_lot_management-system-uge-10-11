namespace Parking_lot_management_system_uge_10_11.DTO
{
    public class UserDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; }
        public int UserTypeID { get; set; }
        public int OrganisationId { get; set; }

    }
}
