﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for TicketBD
/// </summary>
public class TicketBD
{
    public static DataSet Select()
    {
        DataSet ds = new DataSet();
        IDbConnection objConection;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT * FROM ticket ORDER BY tic_openTime DESC;";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
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
    public DataSet SelectOne(String ticketID)
    {
        DataSet ds = new DataSet();
        Ticket ticket = new Ticket();

        IDbConnection objConexao;
        IDbCommand objComando;
        IDataAdapter objAdapter;
        objConexao = Mapped.Connection();
        string sql = "SELECT * FROM ticket WHERE tic_id=?cod;";
        objComando = Mapped.Command(sql, objConexao);
        objAdapter = Mapped.Adapter(objComando);
        objComando.Parameters.Add(Mapped.Parameter("?cod", ticketID));
        objAdapter.Fill(ds);
        objComando.Dispose();
        objConexao.Close();
        objConexao.Dispose();
        return ds;
    }

    public static int UpdateTicket(int value, int idTicket)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"UPDATE ticket SET tic_status=?value WHERE tic_id=?idTicket;";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?value", value));
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