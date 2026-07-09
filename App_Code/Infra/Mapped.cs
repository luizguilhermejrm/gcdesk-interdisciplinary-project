using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

/// <summary>Factory for ADO.NET MySQL objects (connection, command, adapter, parameter).</summary>
public class Mapped
{
    /// <summary>Opens and returns a new MySQL connection using the connection string from Web.config.</summary>
    public static IDbConnection Connection()
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["stringConection"]);
        conn.Open();
        return conn;
    }

    /// <summary>Creates a command object for the given SQL query and connection.</summary>
    public static IDbCommand Command(string query, IDbConnection conection)
    {
        IDbCommand command = conection.CreateCommand();
        command.CommandText = query;
        return command;
    }

    /// <summary>Creates a data adapter for the given command (used with FillDataSet).</summary>
    public static IDataAdapter Adapter(IDbCommand command)
    {
        IDbDataAdapter adap = new MySqlDataAdapter();
        adap.SelectCommand = command;
        return adap;
    }

    /// <summary>Creates a named MySQL parameter.</summary>
    public static IDbDataParameter Parameter(string name, object value)
    {
        return new MySqlParameter(name, value);

    }

}
