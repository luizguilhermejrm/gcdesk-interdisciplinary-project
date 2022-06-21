using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
/// 
/// Nesta classe contém métodos relacionado a ações dos Chamados:
///     - Selects em geral, Inserts, Updates e Autenticação.
///     
/// </summary>
public class TicketBD
{
    /// <summary>
    ///  --> Metodo sendo utilizado na  <--
    /// </summary>
    /// <returns>Retorna os Chamados que estao em aberto</returns>
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

    /// <summary>
    ///
    /// --> Metodo sendo utilizado na page AllTicket  <--
    /// 
    /// </summary>
    /// <returns>Retorna todos os tickets para os Analistas</returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Colaborador  <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna todos os tickets que o Usuario criou</returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado nos Chamados dos Analistas  <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna os chamados que o Analista aceitou</returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Colaborador <--
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns>Insere no banco de dados o chamado</returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Analista  <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna a quantidade de chamados Finalizados</returns>
    public String SelectFinished(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE user_id =?id AND tic_status=2", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public String SelectProgress(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE user_id =?id AND tic_status=1", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Analista <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna a quantidade de Chamados que estao em aberto</returns>
    public String SelectOpen(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE user_id =?id AND tic_status=0", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna a quantidade de chamados que estao em Progresso</returns>
    public String SelectProgressAnalyst(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL from ticket where ana_analisty_id =?id AND tic_status=1", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Analista  <--
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna a quantidade de chamados que estao finalizados</returns>
    public String SelectFinishedAnalyst(int id)
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL from ticket where ana_analisty_id =?id AND tic_status=2", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Analista  <--
    /// 
    /// </summary>
    /// <returns>Retorna a quantidade dos chamados que estao em Aberto</returns>
    public String SelectAllOpen()
    {
        Ticket obj = new Ticket();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(tic_id) TOTAL FROM ticket WHERE tic_status=0", objConection);
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.TicketId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.TicketId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Default <--
    /// 
    /// </summary>
    /// <param name="ticketID"></param>
    /// <returns></returns>
    public DataSet SelectOne(String ticketID)
    {
        DataSet ds = new DataSet();
        Ticket ticket = new Ticket();

        IDbConnection objConection;
        IDbCommand objCommand;
        IDataAdapter objAdapter;
        objConection = Mapped.Connection();
        string sql = "SELECT * FROM ticket WHERE tic_id=?cod;";
        objCommand = Mapped.Command(sql, objConection);
        objAdapter = Mapped.Adapter(objCommand);
        objCommand.Parameters.Add(Mapped.Parameter("?cod", ticketID));
        objAdapter.Fill(ds);
        objCommand.Dispose();
        objConection.Close();
        objConection.Dispose();
        return ds;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na <--
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="idTicket"></param>
    /// <param name="closeTime"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Analista <--
    /// 
    /// </summary>
    /// <param name="anaid"></param>
    /// <param name="idTicket"></param>
    /// <returns>Atualiza no banco de dados o ticket do Analista</returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Index do Analista <--
    /// 
    /// </summary>
    /// <param name="idTicket"></param>
    /// <param name="timeMessage"></param>
    /// <returns>Insere no banco a notificacao para o chamado que ele foi aceito pelo Analista</returns>
    public int InsertNotificationStatusProgress(int idTicket, string timeMessage)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage, not_Status) 
                        VALUES ('O seu chamado foi aceito, a partir de agora está sendo desenvolvido pelo analista',
                                'Chamado em Andamento', ?tic_id, ?timeMessage, '1');";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?timeMessage", timeMessage));
            dbCommand.Parameters.Add(Mapped.Parameter("?tic_id", idTicket));
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na  AnalystTicke<--
    /// 
    /// </summary>
    /// <param name="idTicket"></param>
    /// <param name="timeMessage"></param>
    /// <returns>Insere no banco o status de finalizado para o chamado</returns>
    public int InsertNotificationStatusFinished(int idTicket, string timeMessage)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage, not_status) 
                        VALUES ('O seu chamado foi finalizado, poderia nos enviar uma avaliação por favor.',
                                'Chamado Finalizado', ?tic_id, ?timeMessage, '2');";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?timeMessage", timeMessage));
            dbCommand.Parameters.Add(Mapped.Parameter("?tic_id", idTicket));
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado nat <--
    /// 
    /// </summary>
    /// <param name="idTicket"></param>
    /// <param name="timeMessage"></param>
    /// <returns>Insere no banco um novo status de Notificacao para o chamado</returns>
    public int InsertNotificationStatusCreate(int idTicket, string timeMessage)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage) 
                        VALUES ('O seu chamado foi criado com sucesso.',
                                'Chamado Criado', ?tic_id, ?timeMessage);";

            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?timeMessage", timeMessage));
            dbCommand.Parameters.Add(Mapped.Parameter("?tic_id", idTicket));
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