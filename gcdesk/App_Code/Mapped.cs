using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

//Importa funções do MySQL
using MySql.Data.MySqlClient;
//Trabalhar com Dataset
using System.Data;
//Permite visualizar o web.config
using System.Configuration;
 
    /// <summary>
    /// Summary description for Mapped
    /// </summary>
    public class Mapped
    {
        //Abrir conexao
        public static IDbConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["stringConection"]);
            conn.Open();
            return conn;
        }

        //Executa comando no BD
        public static IDbCommand Command(string query, IDbConnection conection)
        {
            IDbCommand command = conection.CreateCommand();
            command.CommandText = query;
            return command;
        }

        //Retorna um Adapter (SELECT)
        public static IDataAdapter Adapter(IDbCommand command)
        {
            IDbDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = command;
            return adap;
        }

        //Cria parametro da SQL
        public static IDbDataParameter Parameter(string name, object value)
        {
            return new MySqlParameter(name, value);
        
        }

    }