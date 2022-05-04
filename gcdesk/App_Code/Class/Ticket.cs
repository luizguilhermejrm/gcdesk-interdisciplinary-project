using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Login
/// Class of Login
/// </summary>
public class Ticket
{
    public int Id { get; set; }

    public string Description { get; set; }
    public string Localization { get; set; }
    public string TypeTicket { get; set; }
    public int Status { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public int Grade { get; set; }
    public string Analisty { get; set; }

    public int User_Id { get; set; }
    public int Ana_Analisty { get; set; }


    public Ticket()
    {
    //
    // TODO: Add constructor logic here
    //
    }
}