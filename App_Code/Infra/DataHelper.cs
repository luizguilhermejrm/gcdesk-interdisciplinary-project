using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

/// <summary>Static helper for executing ADO.NET commands with minimal boilerplate.</summary>
public static class DataHelper
{
    /// <summary>Filters out null parameters to avoid ADO.NET errors.</summary>
    private static IDbDataParameter[] FilterParams(IDbDataParameter[] parameters)
    {
        return parameters?.Where(p => p != null).ToArray() ?? new IDbDataParameter[0];
    }

    /// <summary>Executes a query and returns a DataSet.</summary>
    public static DataSet FillDataSet(string sql, params IDbDataParameter[] parameters)
    {
        DataSet ds = new DataSet();
        IDbConnection conn = Mapped.Connection();
        IDbCommand cmd = Mapped.Command(sql, conn);
        foreach (var p in FilterParams(parameters))
            cmd.Parameters.Add(p);
        IDataAdapter adapter = Mapped.Adapter(cmd);
        adapter.Fill(ds);
        cmd.Dispose();
        conn.Close();
        conn.Dispose();
        return ds;
    }

    /// <summary>Executes a non-query SQL command and returns the number of affected rows.</summary>
    public static int ExecuteNonQuery(string sql, params IDbDataParameter[] parameters)
    {
        IDbConnection conn = Mapped.Connection();
        IDbCommand cmd = Mapped.Command(sql, conn);
        foreach (var p in FilterParams(parameters))
            cmd.Parameters.Add(p);
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
        conn.Dispose();
        return result;
    }

    /// <summary>Executes a scalar query and returns the first column of the first row as T.</summary>
    public static T ExecuteScalar<T>(string sql, params IDbDataParameter[] parameters)
    {
        IDbConnection conn = Mapped.Connection();
        IDbCommand cmd = Mapped.Command(sql, conn);
        foreach (var p in FilterParams(parameters))
            cmd.Parameters.Add(p);
        object result = cmd.ExecuteScalar();
        cmd.Dispose();
        conn.Close();
        conn.Dispose();
        return result == DBNull.Value ? default(T) : (T)Convert.ChangeType(result, typeof(T));
    }

    /// <summary>Executes a query and maps each row using the provided function.</summary>
    public static List<T> ExecuteReader<T>(string sql, Func<IDataReader, T> map, params IDbDataParameter[] parameters)
    {
        List<T> list = new List<T>();
        IDbConnection conn = Mapped.Connection();
        IDbCommand cmd = Mapped.Command(sql, conn);
        foreach (var p in FilterParams(parameters))
            cmd.Parameters.Add(p);
        IDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(map(reader));
        reader.Close();
        cmd.Dispose();
        conn.Close();
        conn.Dispose();
        return list;
    }
}
