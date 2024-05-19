using CarWash.Models.Users;

namespace CarWash.Brokers.UserManagement
{
    public interface IUserManagementBroker
    {
        ValueTask<User> InsertUserAsync(User user, string password);
        IQueryable<User> SelectAllUsers();
        ValueTask<User> SelectUserByIdAsync(Guid userId);
        ValueTask<User> UpdateUserAsync(User user);
        ValueTask<User> DeleteUserAsync(User user);
    }
}
