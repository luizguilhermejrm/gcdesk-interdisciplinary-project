using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Ticket
/// Class of Ticket
/// </summary>
public class Ticket
{
    public int TicketId { get; set; }
    public string Description { get; set; }
    public string Localization { get; set; }
    public string TypeTicket { get; set; }
    public string Status { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public string Grade { get; set; }
    public int UserId { get; set; }
    public int AnaId { get; set; }


   
}