using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Interface
{
    public interface IOrganisationRepository
    {
        ICollection<Organisation> GetAllOrganisation();
        Organisation GetOrganisation(int id);
        bool OrganisationExist(int id);
        bool DeleteOrganisation(Organisation organisation);
        bool CreateOrganisation(Organisation organisation);
        bool UpdateOrganisation(Organisation organisation);
        bool save();
    }
}
