using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class Parking_Lot_structursRepository : IParking_Lot_structursRepository
    {
        private readonly DataContext context;
        public Parking_Lot_structursRepository(DataContext Context)
        {
            this.context = Context;
        }
        public bool CreateParking_Lot_Structur(Parking_Lot_Structur parking_Lot_Structur)
        {
            context.parking_Lot_Structurs.Add(parking_Lot_Structur);
            return save();
        }

        public bool Deleteparking_Lot_Structur(Parking_Lot_Structur parking_Lot_Structur)
        {
            context.Remove(parking_Lot_Structur);
            return save();
        }

        public ICollection<Parking_Lot_Structur> GetAllparking_Lot_Structur()
        {
            return context.parking_Lot_Structurs.OrderBy(x => x.Parking_lot_Structur_ID).ToList();
        }

        public Parking_Lot_Structur Getparking_Lot_StructurByID(int id)
        {
            return context.parking_Lot_Structurs.Where(x => x.Parking_lot_Structur_ID == id).FirstOrDefault();
        }

        public ICollection<Parking_Lot_Structur> Getparking_Lot_StructurByOrganisationId(int organisationId)
        {
            return context.parking_Lot_Structurs.Where(x => x.OrganisationId == organisationId).ToList();
        }

        public bool parking_Lot_StructurExist(int id)
        {
            return context.parking_Lot_Structurs.Any(x => x.Parking_lot_Structur_ID == id);
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Updateparking_Lot_Structur(Parking_Lot_Structur existing, Parking_Lot_Structur updated)
        {
            // Update only the fields that are allowed to change
            existing.Name = updated.Name;
            existing.Adress = updated.Adress;
            existing.Total_Available_Lots = updated.Total_Available_Lots;
            existing.Total_Occupied_Lots = updated.Total_Occupied_Lots;
            existing.BasePrice = updated.BasePrice;

            // Save changes (existing is already tracked!)
            return save();
        }

    }
}
