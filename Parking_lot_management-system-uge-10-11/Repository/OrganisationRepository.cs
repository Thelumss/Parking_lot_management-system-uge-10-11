using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly DataContext context;

        public OrganisationRepository(DataContext Context)
        {
            this.context = Context;
        }
        public bool CreateOrganisation(Organisation organisation)
        {
            context.Organisation.Add(organisation);
            return save();
        }

        public bool DeleteOrganisation(Organisation organisation)
        {
            context.Remove(organisation);
            return save();
        }

        public ICollection<Organisation> GetAllOrganisation()
        {
            return context.Organisation.OrderBy(x => x.OrganisationID).ToList();
        }

        public Organisation GetOrganisation(int id)
        {
            return context.Organisation.Where(x => x.OrganisationID == id).FirstOrDefault();
        }

        public bool OrganisationExist(int id)
        {
            return context.Organisation.Any(x => x.OrganisationID == id);
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrganisation(Organisation organisation)
        {
            context.Organisation.Update(organisation);
            return save();
        }
    }
}
