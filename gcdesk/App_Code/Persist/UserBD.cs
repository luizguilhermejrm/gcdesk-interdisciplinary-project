using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for UserBD
/// </summary>
public class UserBD
{
    public User Authenticate(string email, string password)
    {
        User obj = null;
        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;

        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT * FROM user WHERE user_email = ?email and user_password = ?password", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?email", email));
        objCommand.Parameters.Add(Mapped.Parameter("?password", password));
        objDataReader = objCommand.ExecuteReader();

        while (objDataReader.Read())
        {
            obj = new User();
            obj.UserId = Convert.ToInt32(objDataReader["user_id"]);
            obj.Email = Convert.ToString(objDataReader["user_email"]);
            obj.TypeAccess = Convert.ToInt32(objDataReader["user_typeAccess"]);
            obj.FirstLogin = Convert.ToInt32(objDataReader["user_firstLogin"]);
            obj.Password = objDataReader["user_password"].ToString();
        }

        
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return obj;
    }
    public User Select(int id)
    {
        User obj = null;
        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT * FROM user WHERE user_id = ?id",
        objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj = new User();
            obj.UserId = Convert.ToInt32(objDataReader["user_id"]);
            obj.Email = Convert.ToString(objDataReader["user_email"]);
            obj.TypeAccess = Convert.ToInt32(objDataReader["user_typeAccess"]);
            obj.FirstLogin = Convert.ToInt32(objDataReader["user_firstLogin"]);
        }
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return obj;
    }

    public static int UpdateUserFirstLogin(User user)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE user SET user_firstLogin=1, user_password=?password WHERE user_id=?userId;";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?password", user.Password));
            dbCommand.Parameters.Add(Mapped.Parameter("?userId", user.UserId));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();

            return 0;
        }
        catch (Exception e)
        {
            return -2;
        }
    }

    public static int UpdateUser(User user)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE user SET user_name=?name,user_position=?position,user_status=1,user_typeAnalyst=teste,user_email=?email,user_password=?password,user_typeAccess=1,user_firstLogin=1, dep_id=?depId WHERE user_id=?userId;";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?name", user.Name));
            dbCommand.Parameters.Add(Mapped.Parameter("?position", user.Position));
            dbCommand.Parameters.Add(Mapped.Parameter("?email", user.Email));
            dbCommand.Parameters.Add(Mapped.Parameter("?password", user.Password));
            dbCommand.Parameters.Add(Mapped.Parameter("?depId", user.DepartId));
            dbCommand.Parameters.Add(Mapped.Parameter("?userId", user.UserId));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();

            return 0;
        }
        catch (Exception e)
        {
            return -2;
        }
    }



    public static DataSet SelectAll()
    {
        DataSet ds = new DataSet();
        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT * FROM user WHERE user_typeAccess = '1'";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConection.Close();
        objConection.Dispose();
        return ds;
    }

    public String SelectQuantityPerson()
    {
        User obj = new User();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(user_id) TOTAL FROM user WHERE user_typeAccess =1", objConexao);
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.UserId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.UserId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    public static int Insert(User user)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO user VALUES(0, ?name, ?position, 1, null, ?email, ?password, 1, 0, ?depId);";
            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?name", user.Name));
            dbCommand.Parameters.Add(Mapped.Parameter("?position", user.Position));
            dbCommand.Parameters.Add(Mapped.Parameter("?email", user.Email));
            dbCommand.Parameters.Add(Mapped.Parameter("?password", user.Password));
            dbCommand.Parameters.Add(Mapped.Parameter("?depId", user.DepartId));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();
            return 0;
        }
        catch (Exception e)
        {
            return -2;
        }
    }
}