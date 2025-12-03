using Microsoft.EntityFrameworkCore;
using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.DTO;
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

        public ICollection<LotHistoryDto> GetLot_HistoryByOrganisationId(int OrganisationId)
        {
            return context.lot_Histories
                .Include(h => h.Lot)
                    .ThenInclude(l => l.Parking_Lot_Structur)
                .Where(h => h.Lot.Parking_Lot_Structur.OrganisationId == OrganisationId)
                .Select(h => new LotHistoryDto
                {
                    License_PLate_Numbers = h.License_PLate_Numbers,
                    Entry_time = h.Entry_time,
                    Exit_time = h.Exit_time,
                    Charged = h.Charged,
                    Active = h.active,
                    Parking_Lot_Structur_Name = h.Lot.Parking_Lot_Structur.Name,
                    Lot_Type = h.Lot.Lot_types.Type
                })
                .ToList();
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

        Lot_History ILot_HistoryRepostiory.GetALot_HistoryByLicence_plate(string License_plate)
        {
            return context.lot_Histories.Where(x => x.License_PLate_Numbers == License_plate).FirstOrDefault();
        }

        public Lot_History GetLot_HistoryByLicence_plateAndActive(string License_plate, bool active)
        {
            return context.lot_Histories.Where(x => (x.License_PLate_Numbers == License_plate) && (x.active == active)).FirstOrDefault();
        }
    }
}
