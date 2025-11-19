using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class UserTypesRepository : IUserTypesRepository
    {
        private readonly DataContext context;
        public UserTypesRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateUserTypes(User_Types user_Types)
        {
            context.user_Types.Add(user_Types);
            return save();
        }

        public bool DeleteUserTypes(User_Types user_Types)
        {
            context.Remove(user_Types);

            return save();
        }

        public User_Types GetUserTypes(int id)
        {
            return context.user_Types.Where(x => x.User_TypesID == id).FirstOrDefault();
        }

        public ICollection<User_Types> GetUser_Types()
        {
            return context.user_Types.OrderBy(x => x.User_TypesID).ToList();
        }

        public bool save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserTypes(User_Types user_types)
        {
            context.user_Types.Update(user_types);
            return save();
        }

        public bool UserTypesExist(int id)
        {
            return context.user_Types.Any(x => x.User_TypesID == id);
        }
    }
}
