using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface ILotRepository
    {
        ICollection<Lot> GetAllLots();
        Lot GetLotbyID(int id);
        ICollection<Lot> GetLotByLotTypeID(int lottypeID);
        ICollection<Lot> GetLotByparking_Lot_StructurId(int parking_Lot_StructurID);
        bool LotsExist(int id);
        bool DeleteLots(Lot lot);
        bool CreateLots(Lot lot);
        bool UpdateLots(Lot lot);
        bool save();

        ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeID(int parking_Lot_StructurID,int lottypeID);
        ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(int parking_Lot_StructurID, int lottypeID,bool occupie_status);
        Lot GetFirstBestLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(int parking_Lot_StructurID, int lottypeID, bool occupie_status);
        ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_StatusAndLotname(int parking_Lot_StructurID, int lottypeID, bool occupie_status,string lotName);
        ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeIDAndLotName(int parking_Lot_StructurID, int lottypeID, string lotName);
        ICollection<Lot> GetLotByparking_Lot_StructurIdAndLotName(int parking_Lot_StructurID, string lotName);


    }
}
