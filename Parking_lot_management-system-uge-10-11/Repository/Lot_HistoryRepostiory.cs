using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class Lot_HistoryRepostiory : ILot_HistoryRepostiory
    {
        private readonly DataContext context;
        public Lot_HistoryRepostiory(DataContext context)
        {
            this.context = context;
        }
        public bool CreateLot_History(Lot_History lot_History)
        {
            context.lot_Histories.Add(lot_History);
            return save();
        }

        public bool DeleteLot_History(Lot_History lot_History)
        {
            context.lot_Histories.Remove(lot_History);
            return save();
        }

        public ICollection<Lot_History> GetAllLot_Historys()
        {
            return context.lot_Histories.OrderBy(x => x.Lot_History_ID).ToList();
        }

        public ICollection<Lot_History> GetLot_HistoryByLicence_plate(string License_plate)
        {
            return context.lot_Histories.Where(x => x.License_PLate_Numbers == License_plate).ToList();
        }

        public Lot_History GetLot_HistoryByID(int id)
        {
            return context.lot_Histories.Where(x => x.Lot_History_ID == id).FirstOrDefault();
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLot_History(Lot_History lot_History)
        {
            context.lot_Histories.Update(lot_History);
            return save();
        }

        public bool Lot_HistoryExist(int id)
        {
            return context.lot_Histories.Any(x => x.Lot_History_ID == id);
        }
    }
}
