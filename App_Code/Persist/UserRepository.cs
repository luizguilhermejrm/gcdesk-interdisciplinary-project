using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for the User entity.</summary>
public class UserRepository : IRepository<User>
{
    /// <summary>Public wrapper for the private Map method (used by external static calls).</summary>
    public static User MapStatic(IDataReader r) { return Map(r); }

    /// <summary>Maps the current row of an IDataReader to a User object.</summary>
    private static User Map(IDataReader r)
    {
        return new User
        {
            UserId = Convert.ToInt32(r["user_id"]),
            Name = r["user_name"].ToString(),
            Position = r["user_position"].ToString(),
            StatusUser = Convert.ToInt32(r["user_status"]),
            TypeAnalist = r["user_typeAnalyst"].ToString(),
            Email = r["user_email"].ToString(),
            Password = r["user_password"].ToString(),
            TypeAccess = Convert.ToInt32(r["user_typeAccess"]),
            FirstLogin = Convert.ToInt32(r["user_firstLogin"]),
            DepartId = Convert.ToInt32(r["dep_id"]),
            Image = r["user_image"].ToString(),
            LastLogin = r["last_login"] == DBNull.Value ? null : r["last_login"].ToString(),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }

    /// <summary>Retrieves a user by their primary key (includes deleted for login checks).</summary>
    public User GetById(int id)
    {
        return DataHelper.ExecuteReader(
            "SELECT * FROM user WHERE user_id=?id",
            r => Map(r),
            Mapped.Parameter("?id", id))[0];
    }

    /// <summary>Returns all active (non-deleted) users.</summary>
    public List<User> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM user WHERE deleted_at IS NULL", r => Map(r));
    }

    /// <summary>Validates user credentials against the database (excludes deleted users).</summary>
    public User Authenticate(string email, string password)
    {
        var users = DataHelper.ExecuteReader(
            "SELECT * FROM user WHERE user_email = ?email AND user_password = ?password AND deleted_at IS NULL",
            r => Map(r),
            Mapped.Parameter("?email", email),
            Mapped.Parameter("?password", password));
        return users.Count > 0 ? users[0] : null;
    }

    /// <summary>Alias for GetById.</summary>
    public User SelectUserTable(int id)
    {
        return GetById(id);
    }

    /// <summary>Updates the password and clears the first-login flag.</summary>
    public static int UpdateUserFirstLogin(User user)
    {
        try
        {
            return DataHelper.ExecuteNonQuery(
                "UPDATE user SET user_firstLogin=1, user_password=?password, updated_at=?now WHERE user_id=?userId",
                Mapped.Parameter("?password", user.Password),
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?userId", user.UserId));
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Updates all user fields (full update).</summary>
    public bool UpdateUser(User user)
    {
        try
        {
            DataHelper.ExecuteNonQuery(
                @"UPDATE user SET user_name=?name, user_position=?position, user_status=1,
                  user_typeAnalyst='Colaborador', user_email=?email, user_password=?password,
                  user_typeAccess=1, user_firstLogin=0, dep_id=?depId, user_image=?image,
                  updated_at=?now
                  WHERE user_id=?userId",
                Mapped.Parameter("?name", user.Name),
                Mapped.Parameter("?position", user.Position),
                Mapped.Parameter("?email", user.Email),
                Mapped.Parameter("?password", user.Password),
                Mapped.Parameter("?depId", user.DepartId),
                Mapped.Parameter("?image", user.Image),
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?userId", user.UserId));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Returns all active collaborator users (typeAccess = 1, not deleted).</summary>
    public static DataSet SelectAllActive()
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM user WHERE user_typeAccess = '1' AND user_status = '1' AND deleted_at IS NULL");
    }

    /// <summary>Returns all inactive collaborator users (typeAccess = 1, status = 0, not deleted).</summary>
    public static DataSet SelectAllInactive()
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM user WHERE user_typeAccess = '1' AND user_status = '0' AND deleted_at IS NULL");
    }

    /// <summary>Returns the number of active collaborator users as a string.</summary>
    public string SelectQuantityPerson()
    {
        int total = DataHelper.ExecuteScalar<int>(
            "SELECT COUNT(user_id) FROM user WHERE user_typeAccess = 1 AND user_status = 1 AND deleted_at IS NULL");
        return total.ToString();
    }

    /// <summary>Explicit interface implementation — delegates to static Insert.</summary>
    int IRepository<User>.Insert(User entity)
    {
        return Insert(entity);
    }

    /// <summary>Inserts a new user into the database.</summary>
    public static int Insert(User user)
    {
        try
        {
            string now = Function.Now();
            return DataHelper.ExecuteNonQuery(
                @"INSERT INTO user (user_id, user_name, user_position, user_status, user_typeAnalyst,
                  user_email, user_password, user_typeAccess, user_firstLogin, dep_id, user_image,
                  created_at, updated_at)
                  VALUES(0, ?name, ?position, 1, null, ?email, ?password, 1, 0, ?depId, ?image, ?now, ?now)",
                Mapped.Parameter("?name", user.Name),
                Mapped.Parameter("?position", user.Position),
                Mapped.Parameter("?email", user.Email),
                Mapped.Parameter("?password", user.Password),
                Mapped.Parameter("?depId", user.DepartId),
                Mapped.Parameter("?image", user.Image),
                Mapped.Parameter("?now", now));
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Explicit interface implementation — delegates to instance Update.</summary>
    bool IRepository<User>.Update(User entity)
    {
        return Update(entity);
    }

    /// <summary>Updates all fields of an existing user.</summary>
    public bool Update(User entity)
    {
        try
        {
            DataHelper.ExecuteNonQuery(
                @"UPDATE user SET user_name=?name, user_position=?position, user_status=?status,
                  user_typeAnalyst=?typeAnalyst, user_email=?email, user_password=?password,
                  user_typeAccess=?typeAccess, dep_id=?depId, user_image=?image,
                  updated_at=?now
                  WHERE user_id=?id",
                Mapped.Parameter("?name", entity.Name),
                Mapped.Parameter("?position", entity.Position),
                Mapped.Parameter("?status", entity.StatusUser),
                Mapped.Parameter("?typeAnalyst", entity.TypeAnalist),
                Mapped.Parameter("?email", entity.Email),
                Mapped.Parameter("?password", entity.Password),
                Mapped.Parameter("?typeAccess", entity.TypeAccess),
                Mapped.Parameter("?depId", entity.DepartId),
                Mapped.Parameter("?image", entity.Image),
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?id", entity.UserId));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Explicit interface implementation — delegates to SoftDelete.</summary>
    bool IRepository<User>.Delete(int id)
    {
        return SoftDelete(id);
    }

    /// <summary>Soft-deletes a user by setting deleted_at.</summary>
    public bool SoftDelete(int userId)
    {
        try
        {
            DataHelper.ExecuteNonQuery(
                "UPDATE user SET deleted_at=?now WHERE user_id=?userId",
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?userId", userId));
            return true;
        }
        catch
        {
            return true;
        }
    }

    /// <summary>Alias for SoftDelete.</summary>
    public bool DeleteUser(int userId) { return SoftDelete(userId); }

    /// <summary>Updates last_login timestamp on user authentication.</summary>
    public static void UpdateLastLogin(int userId)
    {
        DataHelper.ExecuteNonQuery(
            "UPDATE user SET last_login=?now WHERE user_id=?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", userId));
    }

    /// <summary>Updates profile fields (name, email, position, and optionally password).</summary>
    public bool UpdateProfile(User user)
    {
        try
        {
            string now = Function.Now();
            if (!string.IsNullOrEmpty(user.Password) && user.Password.Length < 50)
            {
                DataHelper.ExecuteNonQuery(
                    "UPDATE user SET user_name=?name, user_email=?email, user_position=?position, user_password=?pass, user_image=?img, updated_at=?now WHERE user_id=?id",
                    Mapped.Parameter("?name", user.Name),
                    Mapped.Parameter("?email", user.Email),
                    Mapped.Parameter("?position", user.Position),
                    Mapped.Parameter("?pass", Function.HashText(user.Password)),
                    Mapped.Parameter("?img", user.Image ?? ""),
                    Mapped.Parameter("?now", now),
                    Mapped.Parameter("?id", user.UserId));
            }
            else
            {
                DataHelper.ExecuteNonQuery(
                    "UPDATE user SET user_name=?name, user_email=?email, user_position=?position, user_image=?img, updated_at=?now WHERE user_id=?id",
                    Mapped.Parameter("?name", user.Name),
                    Mapped.Parameter("?email", user.Email),
                    Mapped.Parameter("?position", user.Position),
                    Mapped.Parameter("?img", user.Image ?? ""),
                    Mapped.Parameter("?now", now),
                    Mapped.Parameter("?id", user.UserId));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Reactivates a user by clearing deleted_at and setting status to 1.</summary>
    public bool UpdateUserActive(int userId)
    {
        try
        {
            DataHelper.ExecuteNonQuery(
                "UPDATE user SET user_status = 1, deleted_at=NULL WHERE user_id = ?userId",
                Mapped.Parameter("?userId", userId));
            return true;
        }
        catch
        {
            return true;
        }
    }
}
