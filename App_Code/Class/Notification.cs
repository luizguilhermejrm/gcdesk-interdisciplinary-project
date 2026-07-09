using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>Represents a notification linked to a ticket.</summary>
public class Notification
{
    /// <summary>Unique notification identifier.</summary>
    public int NotficationId { get; set; }

    /// <summary>Notification body text.</summary>
    public string Description { get; set; }

    /// <summary>Notification heading.</summary>
    public string Title { get; set; }

    /// <summary>Foreign key to the related ticket.</summary>
    public int TicketId { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
}
