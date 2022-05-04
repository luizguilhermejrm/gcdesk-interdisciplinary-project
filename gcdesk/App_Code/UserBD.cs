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
            obj.Id = Convert.ToInt32(objDataReader["user_id"]);
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
            obj.Id = Convert.ToInt32(objDataReader["user_id"]);
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
    public UserBD()
    {
    //
    // TODO: Add constructor logic here
    //
    }
}