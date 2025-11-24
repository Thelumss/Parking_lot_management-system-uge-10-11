using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface IParking_Lot_structursRepository
    {
        ICollection<Parking_Lot_Structur> GetAllparking_Lot_Structur();
        Parking_Lot_Structur Getparking_Lot_StructurByID(int id);
        ICollection<Parking_Lot_Structur> Getparking_Lot_StructurByOrganisationId(int organisationId);
        bool parking_Lot_StructurExist(int id);
        bool Deleteparking_Lot_Structur(Parking_Lot_Structur parking_Lot_Structur);
        bool CreateParking_Lot_Structur(Parking_Lot_Structur parking_Lot_Structur);
        bool Updateparking_Lot_Structur(Parking_Lot_Structur existing, Parking_Lot_Structur updated);
        bool save();
    }
}
