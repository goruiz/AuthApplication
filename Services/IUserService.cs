using AuthApplication.Models;

public interface IUserService
{
    User GetUserByEmail(string email); 
    User GetUserById(int userId); 
    User RegisterUser(User user); 
    bool ActivateUser(int userId); 
    bool UpdateLastLogin(int userId);
    User UpdateUserInformation(User updatedUser); 
    bool VerifyPassword(string email, string password); 
}
