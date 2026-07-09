using System;
using System.Data;

/// <summary>Business logic service for user management.</summary>
public class UserService
{
    private readonly UserRepository _userRepository = new UserRepository();

    /// <summary>Finds a user by their email address.</summary>
    public User FindByEmail(string email)
    {
        var users = DataHelper.ExecuteReader(
            "SELECT * FROM user WHERE user_email = ?email AND deleted_at IS NULL",
            r => UserRepository.MapStatic(r),
            Mapped.Parameter("?email", email));
        return users.Count > 0 ? users[0] : null;
    }

    /// <summary>Authenticates a user by email and password (plain-text, hashed internally).</summary>
    public User Authenticate(string email, string password)
    {
        string hashedPassword = Function.HashText(password);
        User user = _userRepository.Authenticate(email, hashedPassword);
        if (user != null)
            UserRepository.UpdateLastLogin(user.UserId);
        return user;
    }

    /// <summary>Gets a user by their ID.</summary>
    public User GetById(int userId)
    {
        return _userRepository.SelectUserTable(userId);
    }

    /// <summary>Creates a new user with the given plain-text password (hashed before storage).</summary>
    public int Create(User user, string rawPassword)
    {
        user.Password = Function.HashText(rawPassword);
        return UserRepository.Insert(user);
    }

    /// <summary>Updates user details (name, position, email, password, department, image).</summary>
    public bool Update(User user)
    {
        return _userRepository.UpdateUser(user);
    }

    /// <summary>Updates only the password, and sets firstLogin flag to indicate password was changed.</summary>
    public bool UpdatePassword(int userId, string newPassword)
    {
        User user = new User();
        user.UserId = userId;
        user.Password = Function.HashText(newPassword);
        return UserRepository.UpdateUserFirstLogin(user) == 0;
    }

    /// <summary>Deactivates a user (soft delete — sets status to 0).</summary>
    public bool Deactivate(int userId)
    {
        return _userRepository.DeleteUser(userId);
    }

    /// <summary>Reactivates a previously deactivated user.</summary>
    public bool Activate(int userId)
    {
        return _userRepository.UpdateUserActive(userId);
    }

    /// <summary>Returns all active users as a DataSet.</summary>
    public DataSet GetAllActive()
    {
        return UserRepository.SelectAllActive();
    }

    /// <summary>Returns all inactive users as a DataSet.</summary>
    public DataSet GetAllInactive()
    {
        return UserRepository.SelectAllInactive();
    }

    /// <summary>Returns the count of active users as a string.</summary>
    public string GetActiveCount()
    {
        return _userRepository.SelectQuantityPerson();
    }

    /// <summary>Updates user profile fields (name, email, position, and optionally password).</summary>
    public bool UpdateProfile(User user)
    {
        return _userRepository.UpdateProfile(user);
    }
}
