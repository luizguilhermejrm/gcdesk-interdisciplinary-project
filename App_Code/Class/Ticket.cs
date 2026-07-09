using System;

/// <summary>Represents a support ticket / chamado.</summary>
public class Ticket
{
    public int TicketId { get; set; }
    public string Description { get; set; }
    public string Localization { get; set; }
    public string TypeTicket { get; set; }
    public int Priority { get; set; }
    public int Status { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public int Grade { get; set; }
    public int Rating { get; set; }
    public string RatingComment { get; set; }
    public string RatingAt { get; set; }
    public int UserId { get; set; }
    public int AnalystId { get; set; }
    public int EquipId { get; set; }
    public int SlaBreached { get; set; }
    public string LastResponseAt { get; set; }
    public int ReopenCount { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
}
