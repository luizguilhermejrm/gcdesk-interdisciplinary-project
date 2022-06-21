using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// 
/// Nesta classe contém métodos relacionado a ações dos Usuários:
///     - Selects em geral, Inserts, Updates e Autenticação.
///     
/// </summary>
public class UserBD
{
    /// <summary>
    /// 
    ///  --> Metodo sendo utilizado na Default <--
    /// 
    /// </summary>
    /// <param name="email"> Recebe a informação pela Sessão o email do usuario logado </param>
    /// <param name="password"> Recebe a informação pela Sessão a senha do usuario logado </param>
    /// <returns> Autentica o tipo do usuario (se é analista ou colaborador) e verifica se é o seu primeiro login. </returns>
    public User Authenticate(string email, string password)
    {
        User obj = null;
        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;

        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT * FROM user WHERE user_email = ?email and user_password = ?password", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?email", email));
        objCommand.Parameters.Add(Mapped.Parameter("?password", password));
        objDataReader = objCommand.ExecuteReader();

        while (objDataReader.Read())
        {
            obj = new User();
            obj.UserId = Convert.ToInt32(objDataReader["user_id"]);
            obj.Name = Convert.ToString(objDataReader["user_name"]);
            obj.Position = Convert.ToString(objDataReader["user_position"]);
            obj.StatusUser = Convert.ToInt32(objDataReader["user_status"]);
            obj.TypeAnalist = Convert.ToString(objDataReader["user_typeAnalyst"]);
            obj.Email = Convert.ToString(objDataReader["user_email"]);
            obj.Password = objDataReader["user_password"].ToString();
            obj.TypeAccess = Convert.ToInt32(objDataReader["user_typeAccess"]);
            obj.FirstLogin = Convert.ToInt32(objDataReader["user_firstLogin"]);
            obj.DepartId = Convert.ToInt32(objDataReader["dep_id"]);
            obj.Image = Convert.ToString(objDataReader["user_image"]);
        }

        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return obj;
    }

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na UpdateUser <--
    /// 
    /// </summary>
    /// <param name="id"> Recebe a informação pela Sessão o id do usuario selecionado na tabela </param>
    /// <returns>Ele retorna os dados do usuario</returns>
    public User SelectUserTable(int id)
    {
        User obj = null;

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT * FROM user WHERE user_id=?id", objConection);
        objCommand.Parameters.Add(Mapped.Parameter("?id", id));
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.UserId = Convert.ToInt32(objDataReader["user_id"]);
            obj.Name = Convert.ToString(objDataReader["user_name"]);
            obj.Position = Convert.ToString(objDataReader["user_position"]);
            obj.StatusUser = Convert.ToInt32(objDataReader["user_status"]);
            obj.TypeAnalist = Convert.ToString(objDataReader["user_typeAnalyst"]);
            obj.Email = Convert.ToString(objDataReader["user_email"]);
            obj.Password = objDataReader["user_password"].ToString();
            obj.TypeAccess = Convert.ToInt32(objDataReader["user_typeAccess"]);
            obj.FirstLogin = Convert.ToInt32(objDataReader["user_firstLogin"]);
            obj.DepartId = Convert.ToInt32(objDataReader["dep_id"]);
            obj.Image = Convert.ToString(objDataReader["user_image"]);
        }


        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return obj;
    }

   

   

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na MasterPage <--
    /// 
    /// Após o usuário realizar a troca de senha obrigatória, caso o status do firstLogin esteja "0",
    /// ele muda o status para "1", atualizando no banco de dados a sua nova senha.
    /// 
    /// </summary>
    /// <param name="user"> Uma instância da classe Usuário para receber as informações </param>
    /// <returns> Realizar a atualização do primeiro login para o status user_firstLogin para "1" e sua atualização de senha </returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na UpdateUser <--
    /// 
    /// </summary>
    /// <param name="user"> Uma instância da classe Usuário para receber as informações</param>
    /// <returns> realizar a atualização dos dados do usuário </returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na ListCollaborator <--
    /// 
    /// </summary>
    /// <returns> Retorna todos os usuários cujo são colaboradores </returns>
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

    /// <summary>
    /// 
    /// --> Metodo sendo utilizado na Dashboard do Analista <--
    /// 
    /// </summary>
    /// <returns> Retorna a quantidade de colaboradores cadastrados no sistema </returns>
    public String SelectQuantityPerson()
    {
        User obj = new User();

        System.Data.IDbConnection objConection;
        System.Data.IDbCommand objCommand;
        System.Data.IDataReader objDataReader;
        objConection = Mapped.Connection();
        objCommand = Mapped.Command("SELECT count(user_id) TOTAL FROM user WHERE user_typeAccess =1", objConection);
        objDataReader = objCommand.ExecuteReader();
        while (objDataReader.Read())
        {
            obj.UserId = Convert.ToInt32(objDataReader["TOTAL"]);

        }
        string resultado = Convert.ToString(obj.UserId);
        objDataReader.Close();
        objConection.Close();
        objCommand.Dispose();
        objConection.Dispose();
        objDataReader.Dispose();
        return resultado;
    }

    /// <summary>
    /// 
    /// --> Metodo utilizado na ListCollaborator <--
    /// 
    /// </summary>
    /// <param name="user"> Uma instância da classe Usuário para receber as informações </param>
    /// <returns> Cadastra as informações do usuário (colaborador) </returns>
    public static int Insert(User user)
    {
        try
        {
            IDbConnection dbConnection;
            IDbCommand dbCommand;
            string sql = @"INSERT INTO user VALUES(0, ?name, ?position, 1, null, ?email, ?password, 1, 0, ?depId, ?image);";
            dbConnection = Mapped.Connection();
            dbCommand = Mapped.Command(sql, dbConnection);
            dbCommand.Parameters.Add(Mapped.Parameter("?name", user.Name));
            dbCommand.Parameters.Add(Mapped.Parameter("?position", user.Position));
            dbCommand.Parameters.Add(Mapped.Parameter("?email", user.Email));
            dbCommand.Parameters.Add(Mapped.Parameter("?password", user.Password));
            dbCommand.Parameters.Add(Mapped.Parameter("?depId", user.DepartId));
            dbCommand.Parameters.Add(Mapped.Parameter("?image", user.Image));
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
    /// --> Metodo utilizado na ListCollaborator <--
    /// -- Metodo que impletementa um SoftDelete, onde desabilita o status do usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Faz um update do status do usuario para 0 </returns>
    public bool DeleteUser(int id)
    {
        IDbConnection dbConnection;
        IDbCommand dbCommand;
        string sql = "UPDATE user SET user_status = 0 WHERE (user_id = ?userId);";
        dbConnection = Mapped.Connection();
        dbCommand = Mapped.Command(sql, dbConnection);
        dbCommand.Parameters.Add(Mapped.Parameter("?userId", id));

        dbCommand.ExecuteNonQuery();
        dbConnection.Close();
        dbCommand.Dispose();
        dbConnection.Dispose();
        return true;

    }

   
}