using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface IUserRepository
    {
        ICollection<Users> GetAllUsers();
        Users GetUsersbyID(int id);
        ICollection<Users> GetUsersByTypeID(int typeID);
        ICollection<Users> GetUsersByOrganisationId(int organisationId);
        bool UsersExist(int id);
        bool DeleteUsers(Users users);
        bool CreateUsers(Users users);
        bool UpdateUsers(Users users);
        bool save();
    }
}
