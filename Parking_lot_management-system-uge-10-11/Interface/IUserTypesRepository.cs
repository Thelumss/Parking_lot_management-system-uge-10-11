using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface IUserTypesRepository
    {
        ICollection<User_Types> GetUser_Types();
        User_Types GetUserTypes(int id);
        bool UserTypesExist(int id);
        bool DeleteUserTypes(User_Types user_Types);
        bool CreateUserTypes(User_Types user_Types);
        bool UpdateUserTypes(User_Types user_types);
        bool save();
    }
}
