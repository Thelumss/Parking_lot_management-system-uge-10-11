using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateUsers(Users users)
        {
            context.Users.Add(users);
            return save();
        }

        public bool DeleteUsers(Users users)
        {
            context.Remove(users);
            return save();
        }

        public ICollection<Users> GetAllUsers()
        {
            return context.Users.OrderBy(x=> x.UserID).ToList();
        }

        public Users GetUsersbyID(int id)
        {
            return context.Users.Where(x => x.UserID == id).FirstOrDefault();
        }

        public ICollection<Users> GetUsersByOrganisationId(int organisationId)
        {
            return context.Users.Where(x => x.OrganisationId == organisationId).ToList();
        }

        public ICollection<Users> GetUsersByTypeID(int typeID)
        {
            return context.Users.Where(x => x.UserTypeID == typeID).ToList();
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUsers(Users users)
        {
            context.Users.Update(users);
            return save();
        }

        public bool UsersExist(int id)
        {
            return context.Users.Any(x => x.UserID == id);
        }
    }
}
