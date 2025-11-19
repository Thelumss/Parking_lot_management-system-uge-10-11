using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class LotTypesRepository : ILotTypesRepository
    {

        private readonly DataContext context;
        public LotTypesRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateLotTypes(Lot_types lot_types)
        {
            context.lot_Types.Add(lot_types);
            return save();
        }

        public bool DeleteLotTypes(Lot_types lot_types)
        {
            context.Remove(lot_types);

            return save();
        }

        public Lot_types GetLotTypes(int id)
        {
            return context.lot_Types.Where(x => x.Lot_typesID == id).FirstOrDefault();
        }

        public ICollection<Lot_types> GetAllLot_Types()
        {
            return context.lot_Types.OrderBy(x => x.Lot_typesID).ToList();
        }

        public bool LotTypesExist(int id)
        {
            return context.lot_Types.Any(x => x.Lot_typesID == id);
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLotTypes(Lot_types lot_types)
        {
            context.lot_Types.Update(lot_types);
            return save();
        }
    }
}
