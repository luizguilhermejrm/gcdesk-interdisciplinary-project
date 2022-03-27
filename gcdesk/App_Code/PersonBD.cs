using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for PessoaBD
/// </summary>
public class PersonBD
{
    public Person Autentica(string email, string password)
    {
        Person obj = null;
        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;

        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT * FROM login WHERE log_email = ?email and log_senha = ?password", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?email", email));
        objCommand.Parameters.Add(Mapped.Parameter("?senha", password));
        objDataReader = objCommand.ExecuteReader();

        while (objDataReader.Read())
        {
            obj = new Person();
            obj.Id = Convert.ToInt32(objDataReader["log_id"]);
            obj.Email = Convert.ToString(objDataReader["log_email"]);
            obj.TypeAccess = Convert.ToInt32(objDataReader["log_nivelAcesso"]);
        }
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return obj;
    }
    public Person Select(int id)
    {
        Person obj = null;
        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT * FROM login WHERE log_id = ?id",
        objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj = new Person();
            obj.Id = Convert.ToInt32(objDataReader["log_id"]);
            obj.Email = Convert.ToString(objDataReader["log_email"]);
            obj.TypeAccess = Convert.ToInt32(objDataReader["log_nivelAcesso"]);
        }
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return obj;
    }
    public PersonBD()
    {
    //
    // TODO: Add constructor logic here
    //
    }
}