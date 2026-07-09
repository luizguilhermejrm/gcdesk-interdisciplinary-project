using System;
using System.Data;

public static class ReportRepository
{
    public static DataSet GetTicketReport(string status, string department)
    {
        string sql = @"SELECT T.tic_id AS ID, T.tic_description AS Descricao,
          T.tic_localization AS Localizacao, T.tic_type AS Tipo,
          CASE T.tic_status WHEN 0 THEN 'Aberto' WHEN 1 THEN 'Em Andamento' WHEN 2 THEN 'Finalizado' END AS Status,
          T.tic_openTime AS Abertura, T.tic_closeTime AS Fechamento,
          U.user_name AS Solicitante
          FROM ticket T INNER JOIN user U ON T.user_id = U.user_id
          WHERE (?statusVal = '' OR T.tic_status = ?statusVal)
            AND (?deptVal = '' OR U.dep_id = ?deptVal)
          ORDER BY T.tic_id DESC";

        return DataHelper.FillDataSet(sql,
            Mapped.Parameter("?statusVal", status ?? ""),
            Mapped.Parameter("?deptVal", department ?? ""));
    }
}
