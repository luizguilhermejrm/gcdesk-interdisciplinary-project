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
    public static DataSet SelectTicketOpen()
    {
        DataSet ds = new DataSet();
        IDbConnection objConection;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT * FROM ticket WHERE tic_status=0;";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConection.Close();
        objConection.Dispose();
        return ds;
    }

    public static DataSet SelectTicketAllAnalyst()
    {
        DataSet ds = new DataSet();
        IDbConnection objConection;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT A.tic_id, B.user_name, A.tic_description, A.tic_localization, A.tic_openTime, A.tic_closeTime, A.tic_status FROM ticket A INNER JOIN user B ON A.ana_analisty_id = B.user_id;";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConection.Close();
        objConection.Dispose();
        return ds;
    }

    public static DataSet SelectTicketCollaborator(int id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConection;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT * FROM ticket WHERE user_id=?id ORDER BY tic_id DESC;";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConection.Close();
        objConection.Dispose();
        return ds;
    }

    public static DataSet SelectTicketAna(int id)
    {
        DataSet ds = new DataSet();
        IDbConnection objConection;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT * FROM ticket WHERE ana_analisty_id=?id;";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConection.Close();
        objConection.Dispose();
        return ds;
    }

    public static int Insert(Ticket t)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO ticket VALUES(0, ?description, ?localization, ?type, 0, ?openTime, 0, 0, ?id, null);";
            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?description", t.Description));
            dbCommand.Parameters.Add(Mapped.Parameter("?type", t.TypeTicket));
            dbCommand.Parameters.Add(Mapped.Parameter("?localization", t.Localization));
            dbCommand.Parameters.Add(Mapped.Parameter("?openTime", t.OpenTime));
            dbCommand.Parameters.Add(Mapped.Parameter("?id", t.UserId));
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

    public String SelectFinished(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE user_id =?id AND tic_status=2", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);
   
        }
        string resultado = Convert.ToString( obj.TicketId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    public String SelectProgress(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE user_id =?id AND tic_status=1", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }
    public String SelectOpen(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE user_id =?id AND tic_status=0", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }
    public String SelectProgressAnalyst(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL from ticket where ana_analisty_id =?id AND tic_status=1", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }
    public String SelectFinishedAnalyst(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL from ticket where ana_analisty_id =?id AND tic_status=2", objConexao);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    public String SelectAllOpen()
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConexao;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConexao = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE tic_status=0", objConexao);
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConexao.Close();
        objCommand.Dispose();
        objConexao.Dispose();
        objDataReader.Dispose();
        return resultado;
    }
    public DataSet SelectOne(String ticketID)
    {
        DataSet ds = new DataSet();
        Ticket ticket = new Ticket();

        IDbConnection objConexao;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConexao = Mapped.Connection();
        string sql = "SELECT * FROM ticket WHERE tic_id=?cod;";
        objCommand = Mapped.Command(sql, objConexao);
        objAdapter = Mapped.Adapter(objCommand);
        objCommand.Parameters.Add(Mapped.Parameter("?cod", ticketID));
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int UpdateTicket(int value, int idTicket, string closeTime)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE ticket SET tic_status=?value, tic_closeTime=?close WHERE tic_id=?idTicket;";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?value", value));
            dbCommand.Parameters.Add(Mapped.Parameter("?idTicket", idTicket));
            dbCommand.Parameters.Add(Mapped.Parameter("?close", closeTime));
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

    public int UpdateTicketAnaSt(int anaid, int idTicket)
    {
        try
        {
            UserBD userbd = new UserBD();
            User user = new User();
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE ticket SET tic_status='1', ana_analisty_id=?analista WHERE tic_id=?idTicket;";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?analista", anaid));
            dbCommand.Parameters.Add(Mapped.Parameter("?idTicket", idTicket));
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