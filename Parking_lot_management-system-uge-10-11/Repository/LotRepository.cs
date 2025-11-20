using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class LotRepository : ILotRepository
    {
        private readonly DataContext context;
        public LotRepository(DataContext context)
        {
            this.context = context;
        }
        public bool CreateLots(Lot lot)
        {
            context.lots.Add(lot);
            return save();
        }

        public bool DeleteLots(Lot lot)
        {
            context.Remove(lot);
            return save();
        }

        public ICollection<Lot> GetAllLots()
        {
            return context.lots.OrderBy(x => x.LotID).ToList();
        }

        public Lot GetFirstBestLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(int parking_Lot_StructurID, int lottypeID, bool occupie_status)
        {
            return context.lots.Where(x => (x.Structur_ID == parking_Lot_StructurID) && (x.Lot_types_ID == lottypeID) && (x.Occupied_Status == occupie_status)).FirstOrDefault();
        }

        public Lot GetLotbyID(int id)
        {
            return context.lots.Where(x => x.LotID == id).FirstOrDefault();
        }

        public ICollection<Lot> GetLotByLotTypeID(int lottypeID)
        {
            return context.lots.Where(x => x.Lot_types_ID == lottypeID).ToList();
        }

        public ICollection<Lot> GetLotByparking_Lot_StructurId(int parking_Lot_StructurID)
        {
            return context.lots.Where(x => x.Structur_ID == parking_Lot_StructurID).ToList();
        }

        public ICollection<Lot> GetLotByparking_Lot_StructurIdAndLotName(int parking_Lot_StructurID, string lotName)
        {
            return context.lots.Where(x => (x.Structur_ID == parking_Lot_StructurID) && (x.LotName == lotName)).ToList();
        }

        public ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeID(int parking_Lot_StructurID, int lottypeID)
        {
            return context.lots.Where(x => (x.Structur_ID == parking_Lot_StructurID) && (x.Lot_types_ID == lottypeID)).ToList();
        }

        public ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeIDAndLotName(int parking_Lot_StructurID, int lottypeID, string lotName)
        {
            return context.lots.Where(x => (x.Structur_ID == parking_Lot_StructurID) && (x.Lot_types_ID == lottypeID) && (x.LotName == lotName)).ToList();
        }

        public ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_Status(int parking_Lot_StructurID, int lottypeID, bool occupie_status)
        {
            return context.lots.Where(x => (x.Structur_ID == parking_Lot_StructurID) && (x.Lot_types_ID == lottypeID) && (x.Occupied_Status == occupie_status)).ToList();
        }

        public ICollection<Lot> GetLotByparking_Lot_StructurIdAndLottypeIDAndOccupie_StatusAndLotname(int parking_Lot_StructurID, int lottypeID, bool occupie_status, string lotName)
        {
            return context.lots.Where(x => (x.Structur_ID == parking_Lot_StructurID) && (x.Lot_types_ID == lottypeID) && (x.Occupied_Status == occupie_status)&&(x.LotName ==lotName)).ToList();
        }

        public bool LotsExist(int id)
        {
            return context.lots.Any(x => x.LotID == id);
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLots(Lot lot)
        {
            context.lots.Update(lot);
            return save();
        }
    }
}
