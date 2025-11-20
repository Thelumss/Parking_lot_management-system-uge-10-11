using Parking_lot_management_system_uge_10_11.DTO;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface IAuthService
    {
        Task<Users> RegisterAsync(UserDTO request);
        Task<string> LoginAsync(UserDTO request);
    }
}
