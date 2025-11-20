using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface ILot_HistoryRepostiory
    {
        ICollection<Lot_History> GetAllLot_Historys();
        Lot_History GetLot_HistoryByID(int id);
        bool Lot_HistoryExist(int id);
        bool DeleteLot_History(Lot_History lot_History);
        bool CreateLot_History(Lot_History lot_History);
        bool UpdateLot_History(Lot_History lot_History);
        bool save();
        ICollection<Lot_History> GetLot_HistoryByLicence_plate(string License_plate);
        Lot_History GetALot_HistoryByLicence_plate(string License_plate);
        Lot_History GetLot_HistoryByLicence_plateAndActive(string License_plate, bool active);
    }
}
