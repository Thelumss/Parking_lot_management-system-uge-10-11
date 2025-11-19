using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface ILot_HistoryRepostiory
    {
        ICollection<Lot_History> GetAllUsers();
        Lot_History GetUsersbyID(int id);
        bool UsersExist(int id);
        bool DeleteUsers(Lot_History lot_History);
        bool CreateUsers(Lot_History lot_History);
        bool UpdateUsers(Lot_History lot_History);
        bool save();

        ICollection<Lot_History> GetLotByparking_Lot_StructurIdAndLottypeID(string License_plate);
    }
}
