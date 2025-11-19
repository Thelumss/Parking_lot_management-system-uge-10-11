using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface ILotTypesRepository
    {
        ICollection<Lot_types> GetAllLot_Types();
        Lot_types GetLotTypes(int id);
        bool LotTypesExist(int id);
        bool DeleteLotTypes(Lot_types Lot_types);
        bool CreateLotTypes(Lot_types Lot_types);
        bool UpdateLotTypes(Lot_types Lot_types);
        bool save();
    }
}
