using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for TicketBD
/// </summary>
public class TicketBD
{
    public static int Insert(Ticket t)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO ticket VALUES(0, ?description, ?localization, 0, 0, ?openTime, 0, 0, null, null);";
            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?description", t.Description));
            dbCommand.Parameters.Add(Mapped.Parameter("?localization", t.Localization));
            dbCommand.Parameters.Add(Mapped.Parameter("?openTime", t.OpenTime));
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

    public static DataSet SelecionarTodos()
    {
        DataSet ds = new DataSet();
        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = Mapped.Connection();
        string sql = "SELECT * FROM ticket ORDER BY tic_openTime DESC;";
        objComando = Mapped.Command(sql, objConexao);
        objAdapter = Mapped.Adapter(objComando);
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int UpdateTicket(int idTicket, string description, string localization,int typeTicket,int status,string openTime, string closeTime, int grade, int user_id,int ana_id)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE ticket SET tic_description=?description, tic_localization=?localization, tic_typeTicket=?typeTicket, tic_status=?status, tic_openTime=?openTime, tic_closeTime=?closeTime, tic_grade=?grade, user_id=?user_id, ana_analisty_id=?ana_id WHERE tic_id=idTicket;";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?description", description));
            dbCommand.Parameters.Add(Mapped.Parameter("?localization", localization));
            dbCommand.Parameters.Add(Mapped.Parameter("?openTime", openTime));
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            dbCommand.Dispose();
            dbConnection.Dispose();

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);

            return -2;
        }
    }
}