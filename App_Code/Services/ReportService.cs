using System;
using System.Data;

public class ReportService
{
    public DataSet GetTicketReport(string status, string department)
    {
        return ReportRepository.GetTicketReport(status, department);
    }
}
